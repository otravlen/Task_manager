using System.ComponentModel.DataAnnotations;

namespace Task_manager.Models
{
    public class TaskVievModel
    {
        public long Id { get; set; }


        [Display(Name="Название")]
        public string Name { get; set; }

        [Display(Name ="Готовность")]
        public string IsDone { get; set; }

        public bool? IsDoneBool { get; set; }

        [Display(Name ="Приоритет")]
        public string Priority { get; set; }

        [Display(Name="Описание")]
        public string Description { get; set; }


        [Display(Name="Дата создания")]
        public string Created {  get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException(Name, "Укажите название задачи!");
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                throw new ArgumentNullException(Description, "Укажите описание задачи!");
            }
        }
    }
}
