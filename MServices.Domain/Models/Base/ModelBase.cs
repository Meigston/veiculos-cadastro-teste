namespace MServices.Domain.Models.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ModelBase
    {
        public ModelBase()
        {
            if (DateTime.MinValue == this.Inclusao)
            {
                this.Inclusao = DateTime.Now;
            }

            this.Alteracao = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public DateTime Inclusao { get; }

        public DateTime Alteracao { get; }
    }
}
