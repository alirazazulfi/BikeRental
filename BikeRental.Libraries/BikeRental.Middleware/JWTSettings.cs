﻿namespace BikeRental.Middleware
{
    public class JWTSettings
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int AccessTokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationMinutes { get; set; }
        public bool AllowMultipleLoginsFromTheSameUser { get; set; }
        public bool AllowSignoutAllUserActiveClients { get; set; }
    }
}
