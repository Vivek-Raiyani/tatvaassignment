using JobBoard.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Services.Utils;

public static class PasswordHash
{
    public static string PassHash(string password, Users user){
        PasswordHasher<Users> passhash = new();
        return passhash.HashPassword(user, password);
    }

    public static PasswordVerificationResult PasswordVerificationResult(string password, Users user){
        PasswordHasher<Users> passhash = new();
        try{
            PasswordVerificationResult passwordResult = passhash.VerifyHashedPassword(user, user.Password, password);
            return passwordResult;
        }catch(Exception e){
            Console.WriteLine(e.Message);
            return Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed;
        }
        
        
    }

}
