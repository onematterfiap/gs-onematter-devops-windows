using System;

namespace OneMatter.Models
{
    public class Job
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public JobStatus Status { get; private set; }

        // Construtor para criar uma nova vaga
        public Job(string title, string description, string location)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("O título não pode estar vazio.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("A descrição não pode estar vazia.");

            Title = title;
            Description = description;
            Location = location ?? "Não informado"; // Garante que não seja nulo
            Status = JobStatus.Open;
        }

        // Métodos para alterar o estado (regras de negócio)
        public void UpdateDetails(string newTitle, string newDescription, string newLocation)
        {
            if (Status == JobStatus.Closed)
                throw new InvalidOperationException("Não é possível atualizar uma vaga fechada.");

            Title = newTitle;
            Description = newDescription;
            Location = newLocation;
        }

        public void CloseJob()
        {
            Status = JobStatus.Closed;
        }

        public enum JobStatus { Open, Closed }

        private Job()
        {
            Title = null!;
            Description = null!;
            Location = null!;
        }
    }
}