using LibplctagWrapper;
using System;

namespace PlantControl
{
    public class PLCRead
    {
        Libplctag client = new Libplctag();

        const int DataTimeout = 5000;

        public int ReadPLCBool(string PlcIP, string TagNameBool)
        {
            try
            {
                //Declare Boolean Tag
                var tag = new Tag(PlcIP, CpuType.MICRO800, TagNameBool, DataType.Int8, 1);

                //Create the tag                                                                   
                client.AddTag(tag);

                client.ReadTag(tag, DataTimeout);

                //Read value of tag BOOL
                var tagResult = client.GetInt8Value(tag, (0));

                client.RemoveTag(tag);

                return tagResult;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public Int16 ReadPLCInt(string PlcIP, string TagNameInt)
        {
            try
            {
                //Declare Int Tag
                var tag = new Tag(PlcIP, CpuType.MICRO800, TagNameInt, DataType.Int16, 1);

                //Create the tag                                                                   
                client.AddTag(tag);

                client.ReadTag(tag, DataTimeout);

                //Read value of tag Int
                var tagResult = client.GetInt16Value(tag, (0));

                client.RemoveTag(tag);

                return tagResult;
            }
            catch (Exception)
            {
                return 0;
            }

        }
        public Int32 ReadPLCDint(string PlcIP, string TagNameDint)
        {
            try
            {
                //Declare Dint Tag
                var tag = new Tag(PlcIP, CpuType.MICRO800, TagNameDint, DataType.Int32, 1);

                //Create the tag                                                                   
                client.AddTag(tag);

                client.ReadTag(tag, DataTimeout);

                //Read value of tag Dint
                var tagResult = client.GetInt32Value(tag, (0));

                client.RemoveTag(tag);

                return tagResult;
            }
            catch (Exception)
            {
                return 0;
            }

        }
        public float ReadPLCReal(string PlcIP, string TagNameReal)
        {
            try
            {
                //Declare Dint Tag
                var tag = new Tag(PlcIP, CpuType.MICRO800, TagNameReal, DataType.Float32, 1);

                //Create the tag                                                                   
                client.AddTag(tag);

                client.ReadTag(tag, DataTimeout);

                //Read value of tag Dint
                var tagResult = client.GetFloat32Value(tag, (0));

                client.RemoveTag(tag);

                return tagResult;
            }
            catch (Exception)
            {
                return 0;
            }

        }
    }
}