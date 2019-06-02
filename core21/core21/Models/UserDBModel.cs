using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace core21.Models
{
    public class UserDBModel
    {
        //register user by username and password
        public UserModel UserRegister(string username, string password)
        {
            string connectionString = "server=localhost;userid=root;password=123456;database=novel";
            if (!UserExisting(username, password))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        string insertData = "insert into user_infor(UserName,Password,Date_of_birth,Image_profile," +
                                            "Coin, Level, Experience, Type, Power, Nick_name) values "
                                            + "(@UserName, @Password, @Date_of_birth, @Image_profile, @Coin, @Level, @Experience, @Type, @Power, @Nick_name)";
                        MySqlCommand command = new MySqlCommand(insertData, connection);

                        command.Parameters.AddWithValue("@UserName", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Date_of_birth", "1993-11-11");
                        command.Parameters.AddWithValue("@Image_profile", "#");
                        command.Parameters.AddWithValue("@Coin", 100);
                        command.Parameters.AddWithValue("@Level", 1);
                        command.Parameters.AddWithValue("@Experience", 100);
                        command.Parameters.AddWithValue("@Type", "normal");
                        command.Parameters.AddWithValue("@Power", 100);
                        command.Parameters.AddWithValue("@Nick_name", "Lee");

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        return null;
                    }

                }
            }
            else
            {
                return null;
            }

            return UserLogin(username, password);
        }

        public bool RemoveUserById(string userid)
        {
            string connectionString = "server=localhost;userid=root;password=123456;database=novel";

            if (AdminChecking(Int32.Parse(userid))) return false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    string insertData = "DELETE FROM user_infor WHERE id=@id";
                    MySqlCommand command = new MySqlCommand(insertData, connection);
                    command.Parameters.AddWithValue("@id", userid);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }
        //get all user, but only admin can do that
        public List<UserModel> GetAllUser(string adminUsername, string adminPass)
        {
            List<UserModel> list_user = new List<UserModel>();
            if(AdminChecking(adminUsername, adminPass))
            {
                string query = "SELECT * FROM user_infor WHERE 1";
                MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=123456;database=novel");

                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();

                var reader = cmd.ExecuteReader();
                
                int i = 0;
                while (reader.Read())
                {
                    UserModel user = new UserModel();
                    user.fID = reader.GetString(0);
                    user.fUsername = reader.GetString(1);
                    user.fPassword = reader.GetString(2);
                    user.fImage_profile = reader.GetString(4);
                    user.fCoin = Int32.Parse(reader.GetString(5));
                    user.fLevel = Int32.Parse(reader.GetString(6));
                    user.fExperience = Int32.Parse(reader.GetString(7));
                    user.fType = reader.GetString(8);
                    user.fPower = Int32.Parse(reader.GetString(9));
                    user.fNick_name = reader.GetString(10);
                    //readNovel.Image_link = reader.GetString(9);

                    list_user.Add(user);
                    i++;
                }

                conn.Close();

                return list_user;
            }
            else
            {
                return list_user;
            }
            
        }

        public bool AdminChecking(string adminUsername, string adminPass)
        {
            UserModel user = UserLogin(adminUsername, adminPass);

            return user.fType == "Admin";
        }
        //true is admin
        public bool AdminChecking(int id)
        {
            UserModel user = GetUserById(id);

            return user.fType == "Admin";
        }

        //checking by username and password
        public bool UserExisting(string username, string password)
        {
            return UserLogin(username, password).fUsername == username; //true for exist user
        }
        //checking by only username
        public bool UserNameExistChecking(string username)
        {
            return UserLogin(username) != null; //true for exist username
        }
        //get user by username and password
        public UserModel UserLogin(string username, string password)
        {
            string query = "SELECT * FROM `user_infor` WHERE UserName ='" + username + "' and Password ='" + password + "'";
            MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=123456;database=novel");

            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();

            var reader = cmd.ExecuteReader();

            UserModel userNovel = new UserModel();
            if (reader.Read())
            {

                userNovel.fID = reader.GetString(0);
                userNovel.fUsername = reader.GetString(1);
                userNovel.fPassword = reader.GetString(2);
                //userNovel.fDate_of_birth = reader.GetString(3);
                userNovel.fImage_profile = reader.GetString(4);

                userNovel.fCoin = Int32.Parse(reader.GetString(5));
                userNovel.fLevel = Int32.Parse(reader.GetString(6));
                userNovel.fExperience = Int32.Parse(reader.GetString(7));
                userNovel.fType = reader.GetString(8);
                userNovel.fPower = Int32.Parse(reader.GetString(9));
                userNovel.fNick_name = reader.GetString(4);
            }

            conn.Close();
            return userNovel;
        }
        //get user by username
        public UserModel UserLogin(string username)
        {
            string query = "SELECT * FROM `user_infor` WHERE UserName ='" + username + "'";
            MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=123456;database=novel");

            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();

            var reader = cmd.ExecuteReader();

            UserModel userNovel = new UserModel();
            if (reader.Read())
            {
                userNovel.fID = reader.GetString(0);
                userNovel.fUsername = reader.GetString(1);
                userNovel.fPassword = reader.GetString(2);
                userNovel.fDate_of_birth = reader.GetString(3);
                userNovel.fImage_profile = reader.GetString(4);

                userNovel.fCoin = Int32.Parse(reader.GetString(5));
                userNovel.fLevel = Int32.Parse(reader.GetString(6));
                userNovel.fExperience = Int32.Parse(reader.GetString(7));
                userNovel.fType = reader.GetString(8);
                userNovel.fPower = Int32.Parse(reader.GetString(9));
                userNovel.fNick_name = reader.GetString(4);
            }

            conn.Close();
            return userNovel;
        }
        //get user by id
        public UserModel GetUserById(int id)
        {
            string query = "SELECT * FROM `user_infor` WHERE id =" + id + "";
            MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=123456;database=novel");

            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();

            var reader = cmd.ExecuteReader();

            UserModel userNovel = new UserModel();
            if (reader.Read())
            {
                userNovel.fID = reader.GetString(0);
                userNovel.fUsername = reader.GetString(1);
                userNovel.fPassword = reader.GetString(2);
                //userNovel.fDate_of_birth = reader.GetString(3);
                userNovel.fImage_profile = reader.GetString(4);
                userNovel.fCoin = Int32.Parse(reader.GetString(5));
                userNovel.fLevel = Int32.Parse(reader.GetString(6));
                userNovel.fExperience = Int32.Parse(reader.GetString(7));
                userNovel.fType = reader.GetString(8);
                userNovel.fPower = Int32.Parse(reader.GetString(9));
                userNovel.fNick_name = reader.GetString(4);
            }

            conn.Close();
            return userNovel;
        }

        //Edit user information
        public bool EditUser(int id, UserModel newUser)
        {
            string connectionString = "server=localhost;userid=root;password=123456;database=novel";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    string insertData = "UPDATE user_infor SET `UserName`=@UserName,"
                        + "`Password`=@Password,`Date_of_birth`=@Date_of_birth,`Image_profile`"
                        + "=@Image_profile,`Coin`=@Coin,`Level`=@Level,`Experience`=@Experience"
                        + ",`Type`=@Type,`Power`=@Power,`Nick_name` = @Nick_name WHERE id=@idold";
                    MySqlCommand command = new MySqlCommand(insertData, connection);
                    //select old novel
                    command.Parameters.AddWithValue("@idold", id);
                    //data updated
                    command.Parameters.AddWithValue("@UserName", newUser.fUsername);
                    command.Parameters.AddWithValue("@Password", newUser.fPassword);
                    command.Parameters.AddWithValue("@Date_of_birth", newUser.fDate_of_birth);
                    command.Parameters.AddWithValue("@Image_profile", newUser.fImage_profile);
                    command.Parameters.AddWithValue("@Coin", newUser.fCoin);
                    command.Parameters.AddWithValue("@Level", newUser.fLevel);
                    command.Parameters.AddWithValue("@Experience", newUser.fExperience);
                    command.Parameters.AddWithValue("@Type", newUser.fType);
                    command.Parameters.AddWithValue("@Power", newUser.fPower);
                    command.Parameters.AddWithValue("@Nick_name", newUser.fNick_name);


                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return false;   
                }
            }


            return true;
        }
    }
}