using Microsoft.EntityFrameworkCore;
using Sugira.Models;

namespace Sugira.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<MenuModel> Menu { get; set; }
    public DbSet<CategoryModel> Category { get; set; }
    public DbSet<ItemModel> Item { get; set; }
    public DbSet<ItemPhotoModel> ItemPhoto { get; set; }
    public DbSet<CharacteristicTypeModel> CharacteristicType { get; set; }
    public DbSet<CharacteristicOptionModel> CharacteristicOption { get; set; }
    public DbSet<ItemCharacteristicModel> ItemCharacteristic { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ItemCharacteristicModel>()
            .HasKey(ic => new { ic.ItemId, ic.CharacteristicTypeId, ic.CharacteristicOptionId });

        modelBuilder.Entity<CharacteristicTypeModel>(entity =>
        {
            entity.HasData(
                new CharacteristicTypeModel { Id = 1, Name = "Amargura" },
                new CharacteristicTypeModel { Id = 2, Name = "Doçura" },
                new CharacteristicTypeModel { Id = 3, Name = "Teor Alcoólico" },
                new CharacteristicTypeModel { Id = 4, Name = "Sabor" },
                new CharacteristicTypeModel { Id = 5, Name = "Textura" },
                new CharacteristicTypeModel { Id = 6, Name = "Cor" },
                new CharacteristicTypeModel { Id = 7, Name = "Carbonatação" },
                new CharacteristicTypeModel { Id = 8, Name = "Ponto" }, // Ponto da carne
                new CharacteristicTypeModel { Id = 9, Name = "Temperatura" },
                new CharacteristicTypeModel { Id = 10, Name = "Tempero" },
                new CharacteristicTypeModel { Id = 11, Name = "Acidez" },
                new CharacteristicTypeModel { Id = 12, Name = "Ardência" },
                new CharacteristicTypeModel { Id = 13, Name = "Volume" },
                new CharacteristicTypeModel { Id = 14, Name = "Tamanho" },
                new CharacteristicTypeModel { Id = 15, Name = "Molho" } 
            );
        });

        modelBuilder.Entity<CharacteristicOptionModel>(entity =>
        {
            entity.HasData(
                // Amargura (TypeId: 1)
                new CharacteristicOptionModel { Id = 1, CharacteristicTypeId = 1, Value = "Leve" },
                new CharacteristicOptionModel { Id = 2, CharacteristicTypeId = 1, Value = "Moderado" },
                new CharacteristicOptionModel { Id = 3, CharacteristicTypeId = 1, Value = "Amargo" },
                new CharacteristicOptionModel { Id = 4, CharacteristicTypeId = 1, Value = "Intenso" },

                // Doçura (TypeId: 2)
                new CharacteristicOptionModel { Id = 5, CharacteristicTypeId = 2, Value = "Baixo" },
                new CharacteristicOptionModel { Id = 6, CharacteristicTypeId = 2, Value = "Equilibrado" },
                new CharacteristicOptionModel { Id = 7, CharacteristicTypeId = 2, Value = "Alto" },

                // Teor Alcoólico (TypeId: 3)
                new CharacteristicOptionModel { Id = 8, CharacteristicTypeId = 3, Value = "Baixo" },
                new CharacteristicOptionModel { Id = 9, CharacteristicTypeId = 3, Value = "Moderado" },
                new CharacteristicOptionModel { Id = 10, CharacteristicTypeId = 3, Value = "Alto" },
                new CharacteristicOptionModel { Id = 11, CharacteristicTypeId = 3, Value = "Muito alto" },

                // Sabor (TypeId: 4)
                new CharacteristicOptionModel { Id = 12, CharacteristicTypeId = 4, Value = "Adocicado" },
                new CharacteristicOptionModel { Id = 13, CharacteristicTypeId = 4, Value = "Cítrico" },
                new CharacteristicOptionModel { Id = 14, CharacteristicTypeId = 4, Value = "Torrado" },
                new CharacteristicOptionModel { Id = 15, CharacteristicTypeId = 4, Value = "Grãos" },
                new CharacteristicOptionModel { Id = 16, CharacteristicTypeId = 4, Value = "Salgado" },
                new CharacteristicOptionModel { Id = 17, CharacteristicTypeId = 4, Value = "Herbal" },
                new CharacteristicOptionModel { Id = 18, CharacteristicTypeId = 4, Value = "Defumado" },
                new CharacteristicOptionModel { Id = 19, CharacteristicTypeId = 4, Value = "Frutado" },
                new CharacteristicOptionModel { Id = 20, CharacteristicTypeId = 4, Value = "Maltado" },

                // Textura (TypeId: 5)
                new CharacteristicOptionModel { Id = 21, CharacteristicTypeId = 5, Value = "Leve" },
                new CharacteristicOptionModel { Id = 22, CharacteristicTypeId = 5, Value = "Cremoso" },
                new CharacteristicOptionModel { Id = 23, CharacteristicTypeId = 5, Value = "Seco" },
                new CharacteristicOptionModel { Id = 24, CharacteristicTypeId = 5, Value = "Encorpado" },
                new CharacteristicOptionModel { Id = 25, CharacteristicTypeId = 5, Value = "Aveludado" },

                // Cor (TypeId: 6)
                new CharacteristicOptionModel { Id = 26, CharacteristicTypeId = 6, Value = "Clara" },
                new CharacteristicOptionModel { Id = 27, CharacteristicTypeId = 6, Value = "Dourada" },
                new CharacteristicOptionModel { Id = 28, CharacteristicTypeId = 6, Value = "Âmbar" },
                new CharacteristicOptionModel { Id = 29, CharacteristicTypeId = 6, Value = "Cobre" },
                new CharacteristicOptionModel { Id = 30, CharacteristicTypeId = 6, Value = "Marrom" },
                new CharacteristicOptionModel { Id = 31, CharacteristicTypeId = 6, Value = "Preta" },

                // Carbonatação (TypeId: 7)
                new CharacteristicOptionModel { Id = 32, CharacteristicTypeId = 7, Value = "Baixa" },
                new CharacteristicOptionModel { Id = 33, CharacteristicTypeId = 7, Value = "Média" },
                new CharacteristicOptionModel { Id = 34, CharacteristicTypeId = 7, Value = "Alta" },

                // Ponto (da carne) (TypeId: 8)
                new CharacteristicOptionModel { Id = 35, CharacteristicTypeId = 8, Value = "Mal passado" },
                new CharacteristicOptionModel { Id = 36, CharacteristicTypeId = 8, Value = "Ao ponto" },
                new CharacteristicOptionModel { Id = 37, CharacteristicTypeId = 8, Value = "Bem passado" },

                // Temperatura (TypeId: 9)
                new CharacteristicOptionModel { Id = 38, CharacteristicTypeId = 9, Value = "Frio" },
                new CharacteristicOptionModel { Id = 39, CharacteristicTypeId = 9, Value = "Morno" },
                new CharacteristicOptionModel { Id = 40, CharacteristicTypeId = 9, Value = "Quente" },

                // Tempero (TypeId: 10)
                new CharacteristicOptionModel { Id = 41, CharacteristicTypeId = 10, Value = "Sem tempero" },
                new CharacteristicOptionModel { Id = 42, CharacteristicTypeId = 10, Value = "Leve" },
                new CharacteristicOptionModel { Id = 43, CharacteristicTypeId = 10, Value = "Moderado" },
                new CharacteristicOptionModel { Id = 44, CharacteristicTypeId = 10, Value = "Intenso" },

                // Acidez (TypeId: 11)
                new CharacteristicOptionModel { Id = 45, CharacteristicTypeId = 11, Value = "Baixa" },
                new CharacteristicOptionModel { Id = 46, CharacteristicTypeId = 11, Value = "Moderada" },
                new CharacteristicOptionModel { Id = 47, CharacteristicTypeId = 11, Value = "Alta" },

                // Ardência (TypeId: 12)
                new CharacteristicOptionModel { Id = 48, CharacteristicTypeId = 12, Value = "Sem ardência" },
                new CharacteristicOptionModel { Id = 49, CharacteristicTypeId = 12, Value = "Leve" },
                new CharacteristicOptionModel { Id = 50, CharacteristicTypeId = 12, Value = "Moderada" },
                new CharacteristicOptionModel { Id = 51, CharacteristicTypeId = 12, Value = "Alta" },

                // Volume (TypeId: 13)
                new CharacteristicOptionModel { Id = 52, CharacteristicTypeId = 13, Value = "269ml" },
                new CharacteristicOptionModel { Id = 53, CharacteristicTypeId = 13, Value = "350ml" },
                new CharacteristicOptionModel { Id = 54, CharacteristicTypeId = 13, Value = "355ml" },
                new CharacteristicOptionModel { Id = 55, CharacteristicTypeId = 13, Value = "473ml" },
                new CharacteristicOptionModel { Id = 56, CharacteristicTypeId = 13, Value = "600ml" },
                new CharacteristicOptionModel { Id = 57, CharacteristicTypeId = 13, Value = "1L" },

                // Tamanho (TypeId: 14)
                new CharacteristicOptionModel { Id = 58, CharacteristicTypeId = 14, Value = "Pequeno" },
                new CharacteristicOptionModel { Id = 59, CharacteristicTypeId = 14, Value = "Médio" },
                new CharacteristicOptionModel { Id = 60, CharacteristicTypeId = 14, Value = "Grande" },

                // Molho (TypeId: 15)
                new CharacteristicOptionModel { Id = 61, CharacteristicTypeId = 15, Value = "Com molho" },
                new CharacteristicOptionModel { Id = 62, CharacteristicTypeId = 15, Value = "Sem molho" }
            );
        });
    }
}