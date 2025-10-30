using UnityEngine;

public class Config 
{
    // Text Title
    public const string Text_Title_Introduction = "Dụng cụ chính:";

    // Text Tutorial
    public const string Step_0 = "Kéo thả nồi đặt trên bếp gas\r\n Kéo thả bình ngưng đặt dưới\r\nống sinh hàn\r\n";
    public const string Step_1 = "Bước 1: Đun hỗn hợp nước và cồn\r\nThao tác:Click chuột vào công tắc\r\nđể bật bếp\r\n";
    public const string Step_2 = "Bước 2: Quan sát hiện tượng:\r\nHơi cồn đi qua ống\r\ngặp vùng lạnh -> bình ngưng\r\n";
    public const string Step_3 = "Bước 3: Quan sát hiện tượng:\r\nHơi cồn ngưng tụ thành chất lỏng\r\n -> thu được cồn";

    public const string AllStep = " Các bước tiến hành:\r\n Bước 1:Đun hỗn hợp nước và cồn\r\n\n Bước 2:Quan sát hiện tượng:\r\n Hơi cồn đi qua ống gặp vùng lạnh\r\n -> bình ngưng\r\n\n Bước 3:Quan sát hiện tượng\r\n Hơi cồn ngưng tụ thành chất lỏng\r\n -> thu được cồn";
    public const string Right = "Đã đặt đúng vị trí";
    public const string Not_Right = "Đặt sai vị trí rồi";
    public const string Completed = "Hoàn thành thí nghiệm";

    // Text Popup
    public const string Text_Reset = "Bạn có muốn\r\nthực hiện lại thí nghiệm";

    // Text Button
    public const string Button_Yes = "Đồng ý";
    public const string Button_No = "Không";

    // Get text of introduction_steps
    public static string GetStepText(int index)
    {
        switch (index)
        {
            case 0: return Step_0;
            case 1: return Step_1;
            case 2: return Step_2;
            case 3: return Step_3;
            default: return Step_3;
        }
    }
}
