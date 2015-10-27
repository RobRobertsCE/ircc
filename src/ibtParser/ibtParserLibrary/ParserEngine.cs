using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ibtParserLibrary
{
    public class ParserEngine
    {
        // 3 header lines
        // 1- start with SOH NULL NULL NULL SOH NULL NULL NULL,  end with EOT NULL NULL NULL
        // 2- start with SOH NULL NULL SOH NULL NULL NULL,  end with EOT NULL NULL NULL
        // 3- start with STX NULL NULL SOH NULL NULL NULL, end with ---
        // data lines:
        public class FieldDescription
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Unit { get; set; }
            public Int32 DataType { get; set; }
            public int Size
            {
                get
                {
                    if (DataType == 1) // 1 = bool
                        return 1;
                    else if (DataType == 2) // 2 = int?
                        return 4;
                    else if (DataType == 3) // 3 = irsdk_EngineWarnings only
                        return 4;
                    else if (DataType == 4) // 4 = float?
                        return 4;
                    else if (DataType == 5) // 5 = float?
                        return 8;
                    else
                        return 0;
                }
            }
            public Int32 Position { get; set; }
            public byte[] bytes
            {
                get; set;
            }
        }

        public static IList<FieldDescription> Fields;

        const byte NUL = 0;
        const byte SOH = 1;
        const byte STX = 2;
        const byte EOT = 4;
        const byte ENQ = 5;
        const byte ACK = 6;
        const byte BEL = 7;
        const byte BS = 8;
        const byte FF = 12; // form feed - On printers, load the next page. Treated as whitespace in many programming languages, and may be used to separate logical divisions in code. In some terminal emulators, it clears the screen.
        const byte DLE = 16;
        const byte DC1 = 17;
        const byte DC2 = 18;
        const byte DC3 = 19;
        const byte DC4 = 20;
        const byte NAK = 21;
        const byte EM = 25; // end of medium - Intended as means of indicating on paper or magnetic tapes that the end of the usable portion of the tape had been reached.
        const byte ESC = 27;
        const byte GS = 29;

        const byte SS3 = 143; // single shift 3 -  Next character invokes a graphic character from the G2 or G3 graphic sets respectively. In systems that conform to ISO/IEC 4873 (ECMA-43), even if a C1 set other than the default is used, these two octets may only be used for this purpose.
        const byte DCS = 144; // device control string - Followed by a string of printable characters (0x20 through 0x7E) and format effectors (0x08 through 0x0D), terminated by ST (0x9C).

        private const int FieldDescriptionLength = 144;

        private const int FieldDescriptionLengthStart = 0;
        private const int FieldDescriptionLengthLength = 2;

        private const int FieldDescriptionPositionStart = 4;
        private const int FieldDescriptionPositionLength = 2;

        private const int FieldDescriptionNameStart = 16;
        private const int FieldDescriptionNameLength = 32;

        private const int FieldDescriptionDescriptionStart = 48;
        private const int FieldDescriptionDescriptionLength = 64;

        private const int FieldDescriptionUnitStart = 112;// FieldDescriptionDescriptionStart + FieldDescriptionDescriptionLength - 8;
        private const int FieldDescriptionUnitLength = 32;

        private const int FieldCountStart = 24;
        private const int FieldCountLength = 2;

        private static ASCIIEncoding ascii = new ASCIIEncoding();

        static byte[] ibtBytes;

        static int fieldCount;

        public static void ParseFile(string fileName)
        {
            try
            {

                ibtBytes = System.IO.File.ReadAllBytes(fileName);
                Fields = new List<FieldDescription>();
                fieldCount = GetIntFromBytes(ibtBytes, FieldCountStart, FieldCountLength);

                // 20th group of 8 bytes starts the field descriptions

                // ReSharper disable once ForCanBeConvertedToForeach
                // ReSharper disable once SuggestVarOrType_BuiltInTypes
                int idx = 0;
                for (int i = 0; i < fieldCount; i++) // for (int i = 0; i < ibtBytes.Length; i++)
                {
                    idx = 144 + (144 * i);
                    ParseFieldDescription(idx);
                }

                // skip text for now, log for three 46's in a row.

                idx++;
                while (true)
                {
                    idx++;
                    if (ibtBytes[idx] == 46)
                    {
                        if ((ibtBytes[idx + 1] == 46) && (ibtBytes[idx + 2] == 46))
                        {
                            idx += 2;
                            break;
                        }
                    }
                }

                idx += 16;

                Console.WriteLine("Starting data parse at position {0}", idx);

                // 561? 556?

                byte[] sampleBytes = new byte[561];
                Array.Copy(ibtBytes, idx, sampleBytes, 0, 561);
                
                foreach (FieldDescription field in Fields)
                {
                    byte[] fieldBytes = new byte[field.Size];
                    Array.Copy(ibtBytes, field.Position, fieldBytes, 0, field.Size);

                    field.bytes = fieldBytes;

                    var valString = String.Empty;
                    if (field.DataType == 1)
                    {
                        var i = BitConverter.ToBoolean(fieldBytes, 0);
                        valString = i.ToString();
                    }
                    else if (field.DataType == 2)
                    {
                        var i = BitConverter.ToSingle(fieldBytes, 0);
                        valString = i.ToString();
                    }
                    else if (field.DataType == 3)
                    {
                        var i = BitConverter.ToSingle(fieldBytes, 0);
                        valString = i.ToString();
                    }
                    else if (field.DataType == 4)
                    {
                        var i = ToSmallDecimal(fieldBytes);
                        //var i = BitConverter.ToSingle(fieldBytes, 0);
                        valString = i.ToString();
                    }
                    else if (field.DataType == 5)
                    {
                        var i = BitConverter.ToDouble(fieldBytes, 0);
                        valString = i.ToString();
                    }
                    Console.WriteLine("{0} {1}", field.Name, valString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static void ParseFieldDescription(int idx)
        {
            byte[] fieldDescriptionBytes = new byte[FieldDescriptionLength];

            Array.Copy(ibtBytes, idx, fieldDescriptionBytes, 0, FieldDescriptionLength);

            FieldDescription field = new FieldDescription();

            field.DataType = GetIntFromBytes(fieldDescriptionBytes, FieldDescriptionLengthStart, FieldDescriptionLengthLength);
            field.Position = GetIntFromBytes(fieldDescriptionBytes, FieldDescriptionPositionStart, FieldDescriptionPositionLength);
            field.Name = GetTextFromBytes(fieldDescriptionBytes, FieldDescriptionNameStart, FieldDescriptionNameLength);
            field.Description = GetTextFromBytes(fieldDescriptionBytes, FieldDescriptionDescriptionStart, FieldDescriptionDescriptionLength);
            field.Unit = GetTextFromBytes(fieldDescriptionBytes, FieldDescriptionUnitStart, FieldDescriptionUnitLength);

            Fields.Add(field);

            //Console.WriteLine("{0,-3}) {1,-32} {2,-64} {3,-32} {4,-4} {5,-4}", Fields.Count.ToString(), field.Name, field.Description, field.Unit, field.DataType.ToString(), field.Position.ToString());
        }

        static string GetTextFromBytes(byte[] bytes, int start, int length)
        {
            return ascii.GetString(bytes, start, length).TrimEnd('\0');
        }

        static int GetIntFromBytes(byte[] bytes, int start, int length)
        {
            byte[] valueBytes = new byte[length];
            Array.Copy(bytes, start, valueBytes, 0, length);
            return (int)(valueBytes[0] + (256 * valueBytes[1]));
        }

        //static int GetIntFromBytes(byte[] bytes, int start, int length)
        //{
        //    byte[] valueBytes = new byte[length];
        //    Array.Copy(bytes, start, valueBytes, 0, length);
        //    int buffer = 0;
        //    for (int i = 0; i < length; i++)
        //    {
        //        buffer += 
        //    }

        //    return (int)(valueBytes[0] + (256 * valueBytes[1]));
        //}

        public static void runtest()
        {
            decimal d = -12.34M;
            byte[] b = GetBytes(d);
            for (int i = 0; i < b.Length; i++)
            {
                Console.WriteLine(b[i].ToString());
            }
            decimal d2 = ToDecimal(b);
        }
        public static decimal ToSmallDecimal(byte[] bytes)
        {
            int[] bits = new int[1];
            bits[0] = ((bytes[0] | (bytes[1] << 8)) | (bytes[2] << 0x10)) | (bytes[3] << 0x18); //lo
            //bits[1] = ((bytes[4] | (bytes[5] << 8)) | (bytes[6] << 0x10)) | (bytes[7] << 0x18); //mid
            //bits[2] = ((bytes[8] | (bytes[9] << 8)) | (bytes[10] << 0x10)) | (bytes[11] << 0x18); //hi
            //bits[3] = ((bytes[12] | (bytes[13] << 8)) | (bytes[14] << 0x10)) | (bytes[15] << 0x18); //flags

            return new decimal(bits);
        }


        public static decimal ToDecimal(byte[] bytes)
        {
            int[] bits = new int[4];
            bits[0] = ((bytes[0] | (bytes[1] << 8)) | (bytes[2] << 0x10)) | (bytes[3] << 0x18); //lo
            bits[1] = ((bytes[4] | (bytes[5] << 8)) | (bytes[6] << 0x10)) | (bytes[7] << 0x18); //mid
            bits[2] = ((bytes[8] | (bytes[9] << 8)) | (bytes[10] << 0x10)) | (bytes[11] << 0x18); //hi
            bits[3] = ((bytes[12] | (bytes[13] << 8)) | (bytes[14] << 0x10)) | (bytes[15] << 0x18); //flags

            return new decimal(bits);
        }

        public static byte[] GetBytes(decimal d)
        {
            byte[] bytes = new byte[16];

            int[] bits = decimal.GetBits(d);
            int lo = bits[0];
            int mid = bits[1];
            int hi = bits[2];
            int flags = bits[3];

            bytes[0] = (byte)lo;
            bytes[1] = (byte)(lo >> 8);
            bytes[2] = (byte)(lo >> 0x10);
            bytes[3] = (byte)(lo >> 0x18);
            bytes[4] = (byte)mid;
            bytes[5] = (byte)(mid >> 8);
            bytes[6] = (byte)(mid >> 0x10);
            bytes[7] = (byte)(mid >> 0x18);
            bytes[8] = (byte)hi;
            bytes[9] = (byte)(hi >> 8);
            bytes[10] = (byte)(hi >> 0x10);
            bytes[11] = (byte)(hi >> 0x18);
            bytes[12] = (byte)flags;
            bytes[13] = (byte)(flags >> 8);
            bytes[14] = (byte)(flags >> 0x10);
            bytes[15] = (byte)(flags >> 0x18);

            return bytes;
        }
    }
}
