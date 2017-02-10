using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SecurityMethod
/// </summary>
public class SecurityMethod
{
    public SecurityMethod()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void AddEmployee()
    {
        var context = new ChinookContext();
        //get all enployees
        var currentEmployee = from x in context.Employees
                              select new EmployeeListPOCO
                              {
                                  empid = x.EmployeeID,
                                  firstname = x.FirstName,
                                  lastname = x.LastName
                              };
        //get all employees who have a logon
        var UserEmployees = from person in Users.ToList()
                            where person.EmployeeID.HasValue
                            select new
                            {
                                userid = person.UserName,
                                empid = person.EmployeeID
                            };
        //get all logon names
        var AllUserNames = from person in Users.ToList()
                            select  person.UserName;
       
        //loop to see if auto generation of employee is needed
        foreach(var emp in currentEmployee)
        {
            //does the employee NOT have a logon (no Users record)
            if (!UserEmployees.Any(ue => ue.empid == emp.empid))
            {
                //suggested new employee UserName
                var newUserName = emp.FirstName.Substring(0, 1) + emp.LastName;
              
                //Generate new userName
                newUserName = CreateNewUserName(AllUserNames, newUserName);
                //current new User and add to Staff role.
                var userAccount = new Application.User()
                {
                    newUserName = newUserName,
                    email = newUserName + "@Chinook.ca"
                };
                this.Create(newUserName, newUserName);
                this.AddToRole(userAccount.Id, "Staff");

            }//eof if

        }//eof loop
    }
    public string CreateNewUserName(List<T> AllUserNames, string newUserName)
    {
        int used = 0;
        int temp = 0; 
        //determine is any currrent username starts with the suggested new employee UserName
        var UsedUserNameList = (from x in AllUserNames.ToList()
                                where x.UserName.StartsWith(newUserName)
                                orderby x.UserName
                                select x).ToList();
        //check if we need to add a number to the end of the suggested new  UserName
        if (UsedUserNameList.Count > 0)
        {
            foreach (var x in UsedUserNameList)
            {
                //get any extra characters beyond the suggested new UserName
                var stripUsedUserName = x.Substring(newUserName.Length);
                //if suggested new userName has a match then new userName will be UserName2 (at least)
                if (x.UserName.Equals(newUserName))
                {
                    used = 1;
                }
                else
                {
                    // eliminate any current user Name containing more alpha characters
                    //than the new suggested name  bobi vs bobbi
                    if (int.TryParse(stripUsedUserName, out temp))
                    {
                        //record the last number attactment used mathcing the new userName
                        //  bobi6 vs bobi, save 6 to be incremeted
                        used = temp;
                    }
                }
            }
        }
        //check to see if number is required for new suggested name
        if (used > 0)
        {
            newUserName += (used + 1).ToString();
        }
        return newUserName;
    }
}