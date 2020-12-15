using TasksApi.Data;
using TasksApi.Models;

namespace TasksApi.Test.Mock
{
    public class DbUnitTestsMockInitializer
    {
        public DbUnitTestsMockInitializer()
        {
        }

        public void Seed(AppDbContext context)
        {
            context.Tasks.Add(new Tasks { Id = 10, Name = "Limpar o carro", IsCompleted = true });
            context.Tasks.Add(new Tasks { Id = 11, Name = "Lavar a moto", IsCompleted = true });
            context.Tasks.Add(new Tasks { Id = 12, Name = "Cortar a grama da casa", IsCompleted = true });
            context.Tasks.Add(new Tasks { Id = 13, Name = "Fazer a barba", IsCompleted = false });
            context.Tasks.Add(new Tasks { Id = 14, Name = "Cortar o cabelo", IsCompleted = true });
            context.Tasks.Add(new Tasks { Id = 15, Name = "Lavar as roupas", IsCompleted = false });
            context.Tasks.Add(new Tasks { Id = 16, Name = "Pagar as contas", IsCompleted = true });
            context.Tasks.Add(new Tasks { Id = 17, Name = "Pagar o mercado", IsCompleted = false });
            context.Tasks.Add(new Tasks { Id = 18, Name = "Fazer compras do mês", IsCompleted = true });
            context.Tasks.Add(new Tasks { Id = 19, Name = "Pesquisar notebook novo", IsCompleted = false });
            context.Tasks.Add(new Tasks { Id = 20, Name = "Adicionar compromissos na agenda", IsCompleted = true });
            context.Tasks.Add(new Tasks { Id = 21, Name = "Limpar a casa", IsCompleted = false });
            context.Tasks.Add(new Tasks { Id = 22, Name = "Regar as plantas", IsCompleted = true });
            context.Tasks.Add(new Tasks { Id = 23, Name = "Comprar sorvete", IsCompleted = false });
            context.Tasks.Add(new Tasks { Id = 24, Name = "Dormir cedo", IsCompleted = true });
            context.Tasks.Add(new Tasks { Id = 25, Name = "Estudar para a prova", IsCompleted = false });
        }
    }
}