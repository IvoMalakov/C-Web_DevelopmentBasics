namespace SharpStore.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SharpStore.Data.SharpStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        //protected override void Seed(SharpStore.Data.SharpStoreContext context)
        //{
        //    IList<Knife> knives = new List<Knife>();

        //    var knife1 = new Knife()
        //    {
        //        Name = "Sharp 3000",
        //        Price = 140,
        //        ImageUrl = "images/blondy1.jpg"
        //    };
        //    knives.Add(knife1);

        //    var knife2 = new Knife()
        //    {
        //        Name = "Sharp4",
        //        Price = 99,
        //        ImageUrl = "images/blondy2.jpg"
        //    };
        //    knives.Add(knife2);

        //    var knife3 = new Knife()
        //    {
        //        Name = "Sharp Ultimate",
        //        Price = 450,
        //        ImageUrl = "images/blondy3.jpg"
        //    };
        //    knives.Add(knife3);

        //    context.Knives.AddRange(knives);
        //    context.SaveChanges();
        //}
    }
}
