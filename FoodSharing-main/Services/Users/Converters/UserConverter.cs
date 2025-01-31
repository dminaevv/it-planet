﻿using FoodSharing.Models.Users;
using Npgsql;

namespace FoodSharing.Services.Users.Converters
{
    public static class UserConverter
	{
		public static async Task<User> MapToUser(NpgsqlDataReader reader)
		{
			User user = new User();

			if (reader.HasRows)
			{
				while (await reader.ReadAsync())
				{
					user.Id = (Guid)reader["ID"];
					user.Email = (string)reader["Email"];
					user.Password = (string)reader["Password"];
					user.CreatedAt = (DateTime)reader["CreatedAt"];
				}
			}
			else
			{
				return null;
			}

			return user;
		}

		public static async Task<List<User>> MapToUsers(NpgsqlDataReader reader)
		{
			List<User> users = new List<User>();

			if (reader.HasRows)
			{
				while (await reader.ReadAsync())
				{
					User user = new User();

					user.Id = (Guid)reader["Id"];
					user.Email = (string)reader["Email"];
					user.Password = (string)reader["Password"];
					user.CreatedAt = (DateTime)reader["CreatedAt"];

					users.Add(user);
				}
			}

			return users;
		}

		public static async Task<UserProfile> MapToUserProfile(NpgsqlDataReader reader)
		{
			UserProfile userProfile = new UserProfile();

			if (reader.HasRows)
			{
				while (await reader.ReadAsync())
				{
					userProfile.Id = (Guid)reader["ID"];
					userProfile.UserId = (Guid)reader["UserId"];
					userProfile.FirstName = reader["FirstName"] is DBNull ? null : (string)reader["FirstName"];
					userProfile.LastName = reader["LastName"] is DBNull ? null : (string)reader["LastName"];
					userProfile.Email = (string)reader["Email"];
					userProfile.Adress = reader["Adress"] is DBNull ? null : (string)reader["Adress"];
					userProfile.Phone = reader["Phone"] is DBNull ? null : (string)reader["Phone"];
					userProfile.Avatar = reader["Avatar"] is DBNull ? null : (byte[])reader["Avatar"];
				}
			}
			else
			{
				return null;
			}

			return userProfile;
		}

		public static UserProfileView MapToUserProfileView(UserProfile userProfile)
		{
			return new UserProfileView(userProfile.Id, userProfile.UserId, userProfile.FirstName, userProfile.LastName, userProfile.Email,
				userProfile.Adress, userProfile.Phone, userProfile.Avatar);
		}
	}    
}