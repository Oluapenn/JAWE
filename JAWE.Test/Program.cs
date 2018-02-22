using System;
using JAWE.Test.Messaging;
using JAWE.Test.Messaging.Parsing;
using JAWE.Test.Messaging.Validators;

namespace JAWE.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "JAWE";

            var something = new ParameterValidator(typeof(TestMessage));
            Console.WriteLine(something);

            byte[] data = { 0x1, 0x1 };
            var binaryParser = new BinaryMessageParser();
            var result = binaryParser.Parse(data);
            Console.WriteLine(result);


            //var processor = new WrMessageProcessor();
            //var wrParser = new WrStrMessageParser(processor);
            //var result = wrParser.Parse("0 1010");
            //Console.WriteLine(result);
            

            Console.ReadLine();
        }
    }
}