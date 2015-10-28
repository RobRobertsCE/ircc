using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ibtParserLibrary
{
    public class ParserEngine
    {
        #region classes
        public class TelemetryFieldDefinition
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
        }

        public class TelemetryFieldValue
        {
            #region props
            public TelemetryFieldDefinition Definition { get; set; }

            public string Value
            {
                get
                {
                    return GetFieldValue().ToString();
                }
            }

            public string ByteString
            {
                get
                {
                    StringBuilder sb = new StringBuilder();
                    for (int s = 0; s < Bytes.Count(); s++)
                    {
                        var hexString = Bytes[s].ToString("X");
                        hexString = (hexString.Length % 2 == 0 ? "" : "0") + hexString + " ";
                        sb.Append(hexString);
                    }
                    return sb.ToString();
                }
            }

            public byte[] Bytes
            {
                get; set;
            }
            #endregion

            #region ctor
            public TelemetryFieldValue()
            {

            }
            public TelemetryFieldValue(TelemetryFieldDefinition definition)
            {
                this.Definition = definition;
            }
            #endregion

            #region private methods
            object GetFieldValue()
            {
                object fieldValue = null;

                switch (Definition.DataType)
                {
                    case 1:
                        {
                            fieldValue = BitConverter.ToBoolean(Bytes, 0);
                            break;
                        }
                    case 2:
                        {
                            fieldValue = BitConverter.ToInt16(Bytes, 0);
                            break;
                        }
                    case 3:
                        {
                            fieldValue = BitConverter.ToInt16(Bytes, 0);
                            break;
                        }
                    case 4:
                        {
                            fieldValue = BitConverter.ToSingle(Bytes, 0);
                            break;
                        }
                    case 5:
                        {
                            fieldValue = BitConverter.ToDouble(Bytes, 0);
                            break;
                        }
                }
                return fieldValue;
            }
            #endregion
        }

        public class TelemetryFieldValueOfT<T>
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

            private T _value;
            public T Value
            {
                get
                {
                    return GetFieldValue();
                }
            }

            public string ByteString
            {
                get
                {
                    StringBuilder sb = new StringBuilder();
                    for (int s = 0; s < bytes.Count(); s++)
                    {
                        var hexString = bytes[s].ToString("X");
                        hexString = (hexString.Length % 2 == 0 ? "" : "0") + hexString + " ";
                        sb.Append(hexString);
                    }
                    return sb.ToString();
                }
            }

            public byte[] bytes
            {
                get; set;
            }

            T GetFieldValue()
            {
                object fieldValue = null;

                switch (DataType)
                {
                    case 1:
                        {
                            fieldValue = BitConverter.ToBoolean(bytes, 0);
                            break;
                        }
                    case 2:
                        {
                            fieldValue = BitConverter.ToInt16(bytes, 0);
                            break;
                        }
                    case 3:
                        {
                            fieldValue = BitConverter.ToInt16(bytes, 0);
                            break;
                        }
                    case 4:
                        {
                            fieldValue = BitConverter.ToSingle(bytes, 0);
                            break;
                        }
                    case 5:
                        {
                            fieldValue = BitConverter.ToDouble(bytes, 0);
                            break;
                        }
                }
                return (T)fieldValue;
            }
        }

        public class TelemetryFrame
        {
            public IList<TelemetryFieldValue> FieldValues { get; set; }

            public TelemetryFrame()
            {
                FieldValues = new List<TelemetryFieldValue>();
            }
        }

        public class TelemetrySession
        {
            #region props
            public IList<TelemetryFrame> Frames { get; set; }
            public IList<TelemetryFieldDefinition> Fields { get; set; }
            public string Yaml { get; set; }
            #endregion

            #region ctor
            public TelemetrySession()
            {
                Frames = new List<TelemetryFrame>();
                Fields = new List<TelemetryFieldDefinition>();
            }
            #endregion

            #region ToString
            public override string ToString()
            {
                return FieldsToString() + Environment.NewLine + ValuesToString();
            }
            public string ValuesToString()
            {
                StringBuilder sb = new StringBuilder();
                foreach (TelemetryFrame tout in Frames)
                {
                    foreach (TelemetryFieldValue tfv in tout.FieldValues)
                    {
                        sb.AppendFormat("{0}: {1} [{2}] ", tfv.Definition.Name, tfv.ByteString, tfv.Value);
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
            }
            public string FieldsToString()
            {
                StringBuilder sb = new StringBuilder();
                int fieldIdx = 0;
                foreach (TelemetryFieldDefinition field in Fields)
                {
                    sb.AppendFormat("{0,-3}) {1,-32} {2,-64} {3,-32} {4,-4} {5,-4}", fieldIdx.ToString(), field.Name, field.Description, field.Unit, field.DataType.ToString(), field.Position.ToString());
                    sb.AppendLine();
                    fieldIdx++;
                }
                return sb.ToString();
            }
            #endregion
        }
        #endregion

        #region fields
        int fieldCount;
        int frameCount;
        private ASCIIEncoding _ascii = new ASCIIEncoding();
        #endregion

        #region props
        public TelemetrySession Session { get; private set; }
        #endregion

        #region consts
        const int FieldDescriptionLength = 144;
        const int FieldDescriptionLengthStart = 0;
        const int FieldDescriptionLengthLength = 2;
        const int FieldDescriptionPositionStart = 4;
        const int FieldDescriptionPositionLength = 2;
        const int FieldDescriptionNameStart = 16;
        const int FieldDescriptionNameLength = 32;
        const int FieldDescriptionDescriptionStart = 48;
        const int FieldDescriptionDescriptionLength = 64;
        const int FieldDescriptionUnitStart = 112;
        const int FieldDescriptionUnitLength = 32;
        const int FieldCountStart = 24;
        const int FieldCountLength = 2;
        const int FrameCountStart = 140;
        const int FrameCountLength = 2;
        #endregion

        #region public methods
        public TelemetrySession ParseTelemetryFile(string fileName)
        {
            int idx = 0;
            Session = new TelemetrySession();

            byte[] telemetryFileBytes = System.IO.File.ReadAllBytes(fileName);

            fieldCount = GetIntFromBytes(telemetryFileBytes, FieldCountStart, FieldCountLength);
            frameCount = GetIntFromBytes(telemetryFileBytes, FrameCountStart, FrameCountLength);

            ParseFieldDescriptionSection(telemetryFileBytes, ref idx);

            ParseYamlSection(telemetryFileBytes, ref idx);

            ParseValueSection(telemetryFileBytes, ref idx);
            
            return Session;
        }
        #endregion
        
        #region Field Description Section
        void ParseFieldDescriptionSection(byte[] telemetryFileBytes, ref int idx)
        {
           for (int i = 0; i < fieldCount; i++)
            {
                idx = FieldDescriptionLength + (FieldDescriptionLength * i);
                ParseFieldDescription(telemetryFileBytes, idx);
            }
            idx += FieldDescriptionLength;
            idx++;
        }

        void ParseFieldDescription(byte[] telemetryFileBytes, int idx)
        {
            byte[] fieldDescriptionBytes = new byte[FieldDescriptionLength];

            Array.Copy(telemetryFileBytes, idx, fieldDescriptionBytes, 0, FieldDescriptionLength);

            TelemetryFieldDefinition field = new TelemetryFieldDefinition();

            field.DataType = GetIntFromBytes(fieldDescriptionBytes, FieldDescriptionLengthStart, FieldDescriptionLengthLength);
            field.Position = GetIntFromBytes(fieldDescriptionBytes, FieldDescriptionPositionStart, FieldDescriptionPositionLength);
            field.Name = GetTextFromBytes(fieldDescriptionBytes, FieldDescriptionNameStart, FieldDescriptionNameLength);
            field.Description = GetTextFromBytes(fieldDescriptionBytes, FieldDescriptionDescriptionStart, FieldDescriptionDescriptionLength);
            field.Unit = GetTextFromBytes(fieldDescriptionBytes, FieldDescriptionUnitStart, FieldDescriptionUnitLength);

            Session.Fields.Add(field);
        }

        string GetTextFromBytes(byte[] bytes, int start, int length)
        {
            return _ascii.GetString(bytes, start, length).TrimEnd('\0');
        }

        int GetIntFromBytes(byte[] bytes, int start, int length)
        {
            byte[] valueBytes = new byte[length];
            Array.Copy(bytes, start, valueBytes, 0, length);
            return (int)(valueBytes[0] + (256 * valueBytes[1]));
        }
        #endregion

        #region YAML Section
        void ParseYamlSection(byte[] telemetryFileBytes, ref int idx)
        {
            idx += 3; // skip the three '-' characters that denote the start of the YAML section.
            int yamlStartIdx = idx;
            // find the three '.' characters that denote the end of the YAML section.
            while (true)
            {
                idx++;
                if (telemetryFileBytes[idx] == 46)
                {
                    if ((telemetryFileBytes[idx + 1] == 46) && (telemetryFileBytes[idx + 2] == 46))
                    {
                        idx += 3;
                        break;
                    }
                }
            }
            int yamlLength = idx - yamlStartIdx - 3; // exclude the three '.' characters on the end.
            Session.Yaml = GetTextFromBytes(telemetryFileBytes, yamlStartIdx, yamlLength);
        }
        #endregion

        #region Value Section
        int ParseValueSection(byte[] telemetryFileBytes, ref int dataStartIdx)
        {
                dataStartIdx += 1;
                var valueLength = telemetryFileBytes.Length - dataStartIdx;
                byte[] frameBytes = new byte[valueLength];
                Array.Copy(telemetryFileBytes, dataStartIdx, frameBytes, 0, valueLength);
                return ParseValues(frameBytes);
        }

        int ParseValues(byte[] valueSectionBytes)
        {
            var startIdx = 0;
            byte[] frameBytes;
            var frameSize = Session.Fields.Max((f) => f.Position + f.Size);
        
            while (true)
            {
                for (int frameByteIndex = startIdx; frameByteIndex < valueSectionBytes.Length - 1; frameByteIndex += frameSize)
                {
                    frameBytes = new byte[frameSize];
                    Array.Copy(valueSectionBytes, frameByteIndex, frameBytes, 0, frameSize);

                    var frame = new TelemetryFrame();
                    foreach (TelemetryFieldDefinition field in Session.Fields)
                    {
                        TelemetryFieldValue fieldValue = new TelemetryFieldValue(field);
                        fieldValue.Bytes = new byte[field.Size];
                        Array.Copy(frameBytes, field.Position, fieldValue.Bytes, 0, field.Size);
                        frame.FieldValues.Add(fieldValue);
                    }

                    Session.Frames.Add(frame);
                }
                break;
            }

            return frameCount;
        }

        object GetFieldValue(int dataType, byte[] bytes)
        {
            object fieldValue = null;

            switch (dataType)
            {
                case 1:
                    {
                        fieldValue = BitConverter.ToBoolean(bytes, 0);
                        break;
                    }
                case 2:
                    {
                        fieldValue = BitConverter.ToInt16(bytes, 0);
                        break;
                    }
                case 3:
                    {
                        fieldValue = BitConverter.ToInt16(bytes, 0);
                        break;
                    }
                case 4:
                    {
                        fieldValue = BitConverter.ToSingle(bytes, 0);
                        break;
                    }
                case 5:
                    {
                        fieldValue = BitConverter.ToDouble(bytes, 0);
                        break;
                    }
            }
            return fieldValue;
        }
        #endregion    
    }
}
