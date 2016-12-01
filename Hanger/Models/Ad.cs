//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hanger.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public partial class Ad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ad()
        {
            this.Photos = new HashSet<Photos>();
            this.Tags = new HashSet<Tags>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Prosz� wprowad� cen�", AllowEmptyStrings = false)]
        [RegularExpression(@"^[-+]?[0-9]*\,?[0-9]+([eE][-+]?[0-9]+)?$", ErrorMessage = "Prosz� wprowad� poprawn� warto��")]
        [DisplayName("Cena")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Prosz� wprowad� tytu�", AllowEmptyStrings = false)]
        [DisplayName("Tytu� og�oszenia")]
        public string Title { get; set; }
        [DisplayName("Opis og�oszenia")]
        public string Description { get; set; }
        public Nullable<System.DateTime> Date_start { get; set; }
        [Required(ErrorMessage = "Prosz� wybierz rozmiar", AllowEmptyStrings = false)]
        [DisplayName("Rozmiar")]
        public int SizeId { get; set; }
        [Required(ErrorMessage = "Prosz� wybierz kolor", AllowEmptyStrings = false)]
        [DisplayName("Kolor")]
        public int ColorId { get; set; }
        [Required(ErrorMessage = "Prosz� wybierz kategori�", AllowEmptyStrings = false)]
        [DisplayName("Kategoria")]
        public int SubcategoryId { get; set; }
        [Required(ErrorMessage = "Prosz� wybierz w jakim stanie jest produkt", AllowEmptyStrings = false)]
        [DisplayName("Stan")]
        public int ConditionId { get; set; }
        [DisplayName("Mo�liwo�� wymiany")]
        public Nullable<bool> Swap { get; set; }
        [DisplayName("Marka")]
        public Nullable<int> BrandId { get; set; }

        public virtual Condition Condition { get; set; }
        public virtual Size Size { get; set; }
        public virtual Color Color { get; set; }
        public virtual User User { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Photos> Photos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tags> Tags { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
