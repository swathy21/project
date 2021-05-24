using System;

namespace CafeAPI.Models
{
    public class Cafe
    {
        public Cafe()
        {

        }
        // Constructor
        // public Student(int sId, string name, string gender, DateTime dob, string c)
        // {
        //     Console.WriteLine($"{sId} {name} {gender} {dob}");
        //     StudentId = sId;
        //     Name = name;
        //     Gender = gender;
        //     DOB = dob;
        //     City = c;
        // }

        public int CafeId { get; set;}
        public int CuisineId{get; set; }
        public int FoodId{get; set;}
        public string CustomerName{get; set;}
        public string FoodName{get; set;}
        public string CuisineType{get;set;}
        public decimal Price{get;set;}
        public string Status{get;set;}

        public static bool IscafeValid(Cafe ns, out string errMsg)
        {
            bool res = IscafeCustomerNameValid(ns.CustomerName, out errMsg);
            if(!res)
            {
                return false;
            }

            errMsg = "";
            return true;
        }

        public static bool IscafeCustomerNameValid(string cafeCustomerName, out string errMsg)
        {
            if (cafeCustomerName.Length <= 1 || cafeCustomerName.Trim().Length <= 1)
            {
                errMsg = "Name cannot be empty. Please input a name";
                return false;
            }
            errMsg = "";
            return true;
        }

        public static bool IscafeIdValid(string strCafeId, out int cafeId, out string errMsg)
        {
            // ******************************************************
            // Validation
            // Check StudentId is not string, > 0 && < 999
            // ******************************************************
            bool res = Int32.TryParse(strCafeId, out cafeId);
            if (!res)  //res == false  //res == true
            {
                errMsg = "Invalid Input. Please input a valid CafeId";
                return false;
            }

            // Check if StudentId > 0
            if (cafeId <= 0)
            {
                errMsg = "cafe Id should be greater than 0";
                return false;
            }

            if (cafeId > 999)
            {
                errMsg = "cafeId should be less than 999";
                return false;
            }
            errMsg = "";
            return true;
        }
    }

}