using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneMatter.Models
{
    public class Candidate
    {
        public int Id { get; private set; }

        // --- Dados Pessoais (Para Anonimizar) ---
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; private set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        public string Email { get; private set; }

        [StringLength(50)]
        public string? Genero { get; private set; } // Ex: "Masculino", "Feminino", "Prefiro não informar"

        [StringLength(255)]
        public string? FotoUrl { get; private set; } // URL da foto hospedada

        // --- Dados Profissionais (Para Exibir) ---
        [Required(ErrorMessage = "As skills são obrigatórias.")]
        public string Skills { get; private set; }

        [Required(ErrorMessage = "A experiência é obrigatória.")]
        public string Experiencia { get; private set; }

        // --- Relacionamentos ---
        public ICollection<JobApplication> Applications { get; private set; }

        private Candidate()
        {
            Nome = null!;
            Email = null!;
            Skills = null!;
            Experiencia = null!;
            Applications = new List<JobApplication>();
            // Genero e FotoUrl podem ser nulos (string?), então não precisam de 'null!'
        }

        // Construtor para criação (usado pela API Java, mas bom ter no .NET para consistência)
        public Candidate(string nome, string email, string skills, string experiencia, string? genero = null, string? fotoUrl = null)
        {
            // Invariantes e Regras de Negócio
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome não pode estar vazio.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("O email não pode estar vazio.");

            if (string.IsNullOrWhiteSpace(skills))
                throw new ArgumentException("O campo 'Skills' não pode estar vazio.");

            if (string.IsNullOrWhiteSpace(experiencia))
                throw new ArgumentException("O campo 'Experiência' não pode estar vazio.");

            Nome = nome;
            Email = email;
            Skills = skills;
            Experiencia = experiencia;
            Genero = genero;
            FotoUrl = fotoUrl;
            Applications = new List<JobApplication>();
        }
    }
}