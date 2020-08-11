using System.ComponentModel.DataAnnotations;

namespace Mvc5LoginSampleApp.Models
{
    public class AuthModel
    {
        //アノテーションと呼ばれる
        //画面上で表示する項目名の設定
        [Display(Name = "ユーザーID")]
        /*
        入力チェック時の必須入力チェック→MVCならアノテーションを設定すれば、
        必須や文字数オーバーなどの基本的な入力チェックを自動でしてくれる
        */
        [Required(ErrorMessage = "ユーザーIDは必須入力です。")]
        public string Id { get; set; }

        [Display(Name = "パスワード")]
        [Required(ErrorMessage = "パスワードは必須入力です。")]
        public string Password { get; set; }
    }
}