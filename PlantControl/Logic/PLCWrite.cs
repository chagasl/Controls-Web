using LibplctagWrapper;
using System;

namespace PlantControl
{
    public class PLCWrite
    {
        Libplctag client = new Libplctag();

        const int DataTimeout = 5000;

        public void WritePLCBool(string PlcIP, string TagNameBool, sbyte setValueBool)
        {
            try
            {
                //Declare Boolean Tag
                var tag = new Tag(PlcIP, CpuType.MICRO800, TagNameBool, DataType.Int8, 1);

                //Create the tag                                                                   
                client.AddTag(tag);

                //Write value to the tag BOOL
                client.SetInt8Value(tag, (0), setValueBool);

                client.WriteTag(tag, DataTimeout);

                client.Dispose();

                client.RemoveTag(tag);
            }
            catch (Exception ex)
            {
                string fault = ex.ToString();
            }
        }
        public void WritePLCInt(string PlcIP, string TagNameInt, short setValueInt)
        {
            try
            {
                //Declare INT Tag
                var tag = new Tag(PlcIP, CpuType.MICRO800, TagNameInt, DataType.Int16, 1);

                //Create the tag                                                                   
                client.AddTag(tag);

                //Write value to the tag INT
                client.SetInt16Value(tag, (0), setValueInt);

                client.WriteTag(tag, DataTimeout);

                client.Dispose();

                client.RemoveTag(tag);
            }
            catch (Exception)
            {
            }
        }
        public void WritePLCDint(string PlcIP, string TagNameDint, int setValueDint)
        {
            try
            {
                //Declare DINT Tag
                var tag = new Tag(PlcIP, CpuType.MICRO800, TagNameDint, DataType.Int32, 1);

                //Create the tag                                                                   
                client.AddTag(tag);

                //Write value to the tag DINT
                client.SetInt32Value(tag, (0), setValueDint);

                client.WriteTag(tag, DataTimeout);

                client.Dispose();

                client.RemoveTag(tag);
            }
            catch (Exception)
            {
            }
        }
        public void WritePLCReal(string PlcIP, string TagNameReal, float setValueReal)
        {
            try
            {
                //Declare REAL Tag
                var tag = new Tag(PlcIP, CpuType.MICRO800, TagNameReal, DataType.Float32, 1);

                //Create the tag                                                                   
                client.AddTag(tag);

                //Write value to the tag REAL
                client.SetFloat32Value(tag, (0), setValueReal);

                client.WriteTag(tag, DataTimeout);

                client.Dispose();

                client.RemoveTag(tag);
            }
            catch (Exception)
            {
            }
        }
    }
}