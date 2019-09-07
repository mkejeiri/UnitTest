using System;
namespace BusinessLogic
{
    public class Manager
    {
        public void AddUser( User user )
        {
            if (user == null) throw  new ArgumentNullException("user must not be null");
            //TODO: put logic here
        }
    }
}