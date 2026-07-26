using System;

class Program
{
    static void Main()
    {
        INotificationService email =
            new EmailService();

        Student student1 =
            new Student("Bizu", email);

        student1.RegisterCourse("ASP.NET Core");

        Console.WriteLine();

        INotificationService sms =
            new SmsService();

        Student student2 =
            new Student("Sara", sms);

        student2.RegisterCourse("C# Advanced");

        Console.WriteLine();

        INotificationService push =
            new PushNotificationService();

        Student student3 =
            new Student("Abel", push);

        student3.RegisterCourse("Entity Framework Core");

        Console.WriteLine();

        INotificationService whatsUp = new WhatsAppService();
        Student student4 = new Student("Mr.x", whatsUp);

        student4.RegisterCourse("Web API Development");

    }
}