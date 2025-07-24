using System;
using System.ComponentModel.DataAnnotations;

namespace anaproject.Models
{
    public class ServiceAppointment
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Ad alanı zorunludur")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Telefon alanı zorunludur")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "E-posta alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Araç modeli zorunludur")]
        public string CarModel { get; set; }
        
        [Required(ErrorMessage = "Plaka zorunludur")]
        public string Plate { get; set; }
        
        [Required(ErrorMessage = "Randevu tarihi zorunludur")]
        public DateTime AppointmentDate { get; set; }
        
        public string Description { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public DateTime CreatedAt { get; set; }
    }
} 