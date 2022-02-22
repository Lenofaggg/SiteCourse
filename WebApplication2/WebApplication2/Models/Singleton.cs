using System;

namespace WebApplication2.Models
{
    public class Singleton
    {
        public  employer employer { get; set; }

        private static Singleton instance;

        protected Singleton(employer emp)
        {
            employer = emp;
        }

        public static Singleton getInstance(employer emp)
        {
            if (instance == null)
                instance = new Singleton(emp);
            return instance;
        }

        public static string Status()
        {
            
            return instance.employer.status_;
        }
    }
}