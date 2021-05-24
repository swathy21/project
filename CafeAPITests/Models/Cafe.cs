using System;
using Newtonsoft.Json;

namespace CafeAPITests.Models
{
    public class Cafe
    {
        public int CafeId { get; set; }
        public int CuisineId{get;set;}
        public int FoodId{get;set;}
        public string CustomerName{get;set;}
        public string FoodName{get;set;}
        public string CuisineType{get;set;}
        public decimal Price{get;set;}
        public string Status{get;set;}




        public override string ToString()
        {
            return  JsonConvert.SerializeObject(this);
        }

        public static bool IsCafeValid(Cafe ns, out string errMsg)
        {
            bool res = IsCafeCustomerNameValid(ns.CustomerName, out errMsg);
            if(!res)
            {
                return false;
            }

            errMsg = "";
            return true;
        }

        public static bool IsCafeCustomerNameValid(string cafeCustomerName, out string errMsg)
        {
            if (cafeCustomerName.Length <= 1 || cafeCustomerName.Trim().Length <= 1)
            {
                errMsg = "Name cannot be empty. Please input a name";
                return false;
            }
            errMsg = "";
            return true;
        }

        public static bool IsCafeIdValid(string strCafeId, out int cafeId, out string errMsg)
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
            if (cafeId <= (int) CafeValue.MinValue)
            {
                errMsg = $"Cafe Id should be greater than {(int) CafeValue.MinValue}";
                return false;
            }

            if (cafeId > (int) CafeValue.MaxValue)
            {
                errMsg = $"Cafe Id should be less than {(int) CafeValue.MaxValue}";
                return false;
            }
            errMsg = "";
            return true;
        }
    }

}