using System;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

public static class SessionExtensions
{
    // Phương thức Set<T> dùng để lưu trữ một giá trị vào session dưới dạng JSON
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value)); // Serialize đối tượng thành chuỗi JSON
    }

    // Phương thức Get<T> để lấy giá trị từ session và giải mã nó từ JSON
    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key); // Lấy chuỗi JSON từ session

        // Kiểm tra nếu không có giá trị trong session, trả về mặc định của kiểu T
        return value == null ? default : JsonSerializer.Deserialize<T>(value); 
    }
}
