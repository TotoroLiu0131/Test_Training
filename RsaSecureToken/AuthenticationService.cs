﻿using System;
using System.Collections.Generic;

namespace RsaSecureToken
{
    public class AuthenticationService
    {
        public bool IsValid(string account, string password)
        {
            // 根據 account 取得設定時間
            var profileDao = new ProfileDao();
            var registerMinutes = profileDao.GetRegisterTimeInMinutes(account);

            // 根據 registerMinutes 取得 RSA token 目前的亂數
            var rsaToken = new RsaTokenDao();
            var otp = rsaToken.GetRandom(registerMinutes);

            // 驗證傳入的 password 是否等於 otp
            var isValid = password == otp;

            if (isValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class ProfileDao
    {
        public int GetRegisterTimeInMinutes(string account)
        {
            // Not Complete yet
            throw new NotImplementedException();
        }
    }

    public class RsaTokenDao
    {
        public string GetRandom(int minutes)
        {
            var seed = new Random((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMinutes - minutes);
            var result = seed.Next(0, 999999).ToString("000000");

            return result;
        }
    }
}