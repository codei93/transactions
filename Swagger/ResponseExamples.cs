using trans_api.DTOs;
using trans_api.Models;

namespace trans_api.Swagger
{
    public class ResponseExamples
    {
        public class UserLogin200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }
            public string Token { get; set; }
            public UserDTO User { get; set; }
        }


        public class User404Response
        {
            ///<example>404</example>
            public int StatusCode { get; set; }

            ///<example>A user with the specified Username was not found.</example>
            public string Message { get; set; }
        }

        public class UserUpdated200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            ///<example>User has been updated.</example>
            public string Message { get; set; }
        }


        public class UserDeleted200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            ///<example>User has been deleted.</example>
            public string Message { get; set; }
        }


        public class User400Response
        {
            ///<example>400</example>
            public int StatusCode { get; set; }

            ///<example>Bad request, .</example>
            public string Message { get; set; }
        }

        public class UserRegistration200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            ///<example>User has been created.</example>
            public string Message { get; set; }
        }


        public class UserRegistration400Response
        {
            ///<example>400</example>
            public int StatusCode { get; set; }

            ///<example>Bad request. A user with such details already exists.</example>
            public string Message { get; set; }
        }

        public class UserPassword200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            ///<example>User password has been updated.</example>
            public string Message { get; set; }
        }

        public class UserPasswordResponse
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            ///<example>Password Updated.</example>
            public string Message { get; set; }
        }

        public class UserPassword400Response
        {
            ///<example>400</example>
            public int StatusCode { get; set; }

            ///<example>Bad request. Current password is not the same as your old password.</example>
            public string Message { get; set; }
        }

        public class UserGetOne200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            public UserDTO User { get; set; }
        }


        public class UserGetAll200Response
        {
            public UserDTO User { get; set; }
        }



        public class RoleCreate200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            ///<example>Role has been created.</example>
            public string Message { get; set; }
        }

        public class RoleGetOne200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            public Role Role { get; set; }
        }

        public class RoleGetAll200Response
        {
        
            public Role Role { get; set; }
        }

        public class RoleUpdate200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            ///<example>Role has been updated.</example>
            public string Message { get; set; }
        }

        public class RoleDelete200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            ///<example>Role has been deleted.</example>
            public string Message { get; set; }
        }


        public class Role404Response
        {
            ///<example>400</example>
            public int StatusCode { get; set; }

            ///<example>A role with the specified Id was not found.</example>
            public string Message { get; set; }
        }


        public class RoleCreate400Response
        {
            ///<example>400</example>
            public int StatusCode { get; set; }

            ///<example>Bad request. A Role Name with such details already exists.</example>
            public string Message { get; set; }
        }


        public class RoleUpdate400Response
        {
            ///<example>400</example>
            public int StatusCode { get; set; }

            ///<example>Bad Request. Role Id does not match.</example>
            public string Message { get; set; }
        }


        public class TransactionCreate200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            ///<example>Transaction has been created.</example>
            public string Message { get; set; }
        }

        public class TransactionUpdate200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            ///<example>Transaction has been updated.</example>
            public string Message { get; set; }
        }

        public class TransactionDelete200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            ///<example>Transaction has been Deleted.</example>
            public string Message { get; set; }
        }

        public class Transaction404Response
        {
            ///<example>404</example>
            public int StatusCode { get; set; }

            ///<example>A transaction with the specified Id was not found.</example>
            public string Message { get; set; }
        }

        public class TransactionGetOne200Response
        {
            ///<example>200</example>
            public int StatusCode { get; set; }

            public Transaction Transaction { get; set; }
        }

        public class TransactionGetAll200Response
        {
            public Transaction Transaction { get; set; }
        }


        public class Transaction400Response
        {
            ///<example>400</example>
            public int StatusCode { get; set; }

            ///<example>Bad request. Transaction Id does not match.</example>
            public string Message { get; set; }
        }



        public class Response401
        {
            ///<example>401</example>
            public int StatusCode { get; set; }

            ///<example>Authorization information is missing or invalid.</example>
            public string Message { get; set; }
        }

        public class Response403
        {
            ///<example>403</example>
            public int StatusCode { get; set; }

            ///<example>Authentication information is missing or invalid.</example>
            public string Message { get; set; }
        }

        public class Response500
        {
            ///<example>500</example>
            public int StatusCode { get; set; }

            ///<example>Unexepected error occurred while processing your request.</example>
            public string Message { get; set; }

            ///<example>String.</example>
            public string Detailed { get; set; }
        }
    }
}
