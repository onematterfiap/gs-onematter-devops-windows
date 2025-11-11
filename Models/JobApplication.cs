using System;
using System.ComponentModel.DataAnnotations;

namespace OneMatter.Models
{
    public class JobApplication
    {
        public int Id { get; private set; }

        [Required]
        public ApplicationStatus Status { get; private set; }

        public DateTime AppliedAt { get; private set; }

        // Pontuação pode ser nula no início
        public int? SkillScore { get; private set; }


        // --- Relacionamento com Job (Vaga) ---
        [Required]
        public int JobId { get; private set; }
        public Job Job { get; private set; }


        // --- Relacionamento com Candidate (Candidato) ---
        [Required]
        public int CandidateId { get; private set; }
        public Candidate Candidate { get; private set; }

        private JobApplication()
        {
            Job = null!;
            Candidate = null!;
        }

        // Construtor para criar uma nova aplicação
        public JobApplication(int jobId, int candidateId)
        {
            if (jobId <= 0)
                throw new ArgumentException("ID da vaga é inválido.");

            if (candidateId <= 0)
                throw new ArgumentException("ID do candidato é inválido.");

            JobId = jobId;
            CandidateId = candidateId;
            Status = ApplicationStatus.Pendente;
            AppliedAt = DateTime.UtcNow;
            SkillScore = null;

            // Também precisamos inicializar aqui para evitar avisos
            Job = null!;
            Candidate = null!;
        }

        // --- Métodos de Regra de Negócio (Mudar Estado) ---
        public void AprovarParaTeste()
        {
            if (Status != ApplicationStatus.Pendente)
            {
                throw new InvalidOperationException("Apenas aplicações 'Pendentes' podem ser aprovadas para o teste.");
            }
            Status = ApplicationStatus.Aprovado_Etapa1;
        }

        public void SubmeterPontuacaoTeste(int score)
        {
            if (Status != ApplicationStatus.Aprovado_Etapa1)
            {
                throw new InvalidOperationException("A pontuação só pode ser submetida para aplicações 'Aprovadas para Teste'.");
            }

            if (score < 0 || score > 100)
            {
                throw new ArgumentException("Pontuação deve estar entre 0 e 100.");
            }

            SkillScore = score;
            Status = ApplicationStatus.Teste_Concluido;
        }

        public void Rejeitar()
        {
            Status = ApplicationStatus.Rejeitado;
        }
    }

    public enum ApplicationStatus
    {
        Pendente,
        Aprovado_Etapa1,
        Teste_Concluido,
        Rejeitado,
        Contratado
    }
}