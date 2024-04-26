using yummyApp.Domain.Common;

namespace yummyApp.Domain.Entities
{
    public class Tag : BaseAuditableEntity<Guid>
    {
        public Guid? PostID { get; set; }
        public Guid? BusinessID { get; set; }
        public Post? Post { get; set; }
        public Business? Business { get; set; }
    }
}
// şöyle bir senaryo için ekledim bunu paylaşımlar, farklı kategorilere (örneğin, yemek, seyahat, teknoloji) ayrılabilir ve kullanıcılar belirli bir kategoriye ait içerikleri görmek için etiketleri kullanabilir.