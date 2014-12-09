using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace LogicController
{
    public partial class Session
    {
        public class Serialer
        {
            public void Save(SerializeSession obj)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(@"data.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, obj);
                stream.Close();
            }

            public SerializeSession Load()
            {
                Stream stream = new FileStream("data.bin", FileMode.Open, FileAccess.Read, FileShare.None);
                IFormatter formatter = new BinaryFormatter();
                SerializeSession items = (SerializeSession)formatter.Deserialize(stream);
                stream.Close();

                return items;
            }
        }

        [Serializable]
        public class SerializeSession
        {
            public Settings Settings { get; set; }
            public DatabaseConnection DatabaseConnection { get; set; }
        }
    }
}
