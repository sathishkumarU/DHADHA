using System;
namespace FirstControllerProject
{
    public class Extension
    {
        public string getEntityId(object Entity)
        {
            return $"{Entity}AutoId";
        }
    }
}