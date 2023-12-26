using System.ComponentModel.DataAnnotations;

namespace Hospilom.Models
{
    public class DoktorTuru
    {
        [Key] //primary key
        public int Id { get; set; }
        //uzmanlık alanı farklı doktorlar için farklı alanlar tanımlamak gerekiyor
        //bu yüzden her hastalık alanı için bir id oluşturacağız ve bu alnalarda işlmeler için
        //Id alanı primary yapıyoruz üsütne yazdığımız [Key] kodu ile yaptık

        [Required] //not null anlamına geliyor
        public string Alan { get; set; }

        //bir id varsa o id ye ait alan ismi olması gerekiyor.

    }
}
