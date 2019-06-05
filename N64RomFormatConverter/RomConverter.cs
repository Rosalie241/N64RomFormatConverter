/* 
 Copyright 2019 Tim Wanders <tim241@mailbox.org>
 Copyright 2016 https://github.com/masl123 (no full name or email has been provided)
 
 The following code is a derivative work of the code from the n64RomConverter project(https://github.com/masl123/n64RomConverter),
 which is licensed LGPLv3. This code therefore is licensed under the terms 
 of the GNU Public License, verison 3.
*/
using System;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64RomFormatConverter
{
    /// <summary>
    /// Simple n64 rom converter
    /// </summary>
    class RomConverter : IDisposable
    {
        public enum RomFormat
        {
            INVALID,
            N64,
            Z64,
            V64
        }

        private string input { get; set; }
        private RomFormat romFormat { get; set; }

        public string RomHeader { get; set; }
        private void getRomInfo()
        {
            // get the header
            byte[] buffer = new byte[4];

            // read first 4 bytes and store them in the buffer
            using(FileStream fileStream = new FileStream(input, FileMode.Open))
            {
                fileStream.Read(buffer, 0, 4);
                fileStream.Close();
            }

            string bufferString = BitConverter.ToString(buffer);
            RomHeader = bufferString;
            switch (bufferString)
            {
                // n64 format
                case "40-12-37-80":
                    romFormat = RomFormat.N64;
                    break;

                // z64 format
                case "80-37-12-40":
                    romFormat = RomFormat.Z64;
                    break;

                // v64 format
                case "37-80-40-12":
                    romFormat = RomFormat.V64;
                    break;

                // invalid
                default:
                    romFormat = RomFormat.INVALID;
                    break;
            }
           
        }
        public RomConverter(string input)
        {
            this.input = input;
            getRomInfo();
        }

        public string GetMd5Hash()
        {
            using(MD5 hash = MD5.Create())
            {
                using(FileStream fileStream = File.OpenRead(input))
                {
                    return BitConverter.ToString(hash.ComputeHash(fileStream)).Replace("-", "").ToLower();
                }
            }
        }
        public void Convert(string outputFile, RomFormat format)
        {
            byte[] fileBytes = File.ReadAllBytes(input);
           
            switch(romFormat)
            {
                case RomFormat.N64:

                    switch (format)
                    {
                        case RomFormat.Z64:
                            dWordSwap(fileBytes);
                            break;

                        case RomFormat.V64:
                            wordSwap(dWordSwap(fileBytes));
                            break;

                        default:
                            break;
                    }

                    break;

                case RomFormat.V64:

                    switch(format)
                    {
                        case RomFormat.N64:
                            dWordSwap(wordSwap(fileBytes));
                            break;

                        case RomFormat.Z64:
                            wordSwap(fileBytes);
                            break;

                        default:
                            break;
                    }

                    break;

                case RomFormat.Z64:

                    switch (format)
                    {
                        case RomFormat.N64:
                            dWordSwap(fileBytes);
                            break;

                        case RomFormat.V64:
                            wordSwap(fileBytes);
                            break;

                        default:
                            break;
                    }

                    break;
            }

            File.WriteAllBytes(outputFile, fileBytes);
        }

        private byte[] wordSwap(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i += 2)
                wordSwap(buffer, i, i + 1);

            return buffer;
        }

        private byte[] wordSwap(byte[] buffer, int a, int b)
        {
            byte temp = buffer[a];
            buffer[a] = buffer[b];
            buffer[b] = temp;

		    return buffer;
        }

        private byte[] dWordSwap(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i += 4)
            {
                wordSwap(buffer, i, i + 3);
                wordSwap(buffer, i + 1, i + 2);
            }

            return buffer;
        }

        public RomFormat GetFormat() => romFormat;

        // Implement IDispose
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
