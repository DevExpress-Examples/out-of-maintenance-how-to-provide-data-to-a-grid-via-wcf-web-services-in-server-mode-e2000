using System;
using System.Collections.Generic;
using DataObjects;
using InterLinq.Objects;

namespace WcfSample {
    public class ExampleObjectSource : IObjectSource {
        public ExampleObjectSource() {
            Objects = new List<WpfServerSideGridTest>();
        }
        public List<WpfServerSideGridTest> Objects { get; set; }
        public IEnumerable<T> GetObjects<T>() {
            if (typeof(T) == typeof(WpfServerSideGridTest)) {
                return (IEnumerable<T>)Objects;
            }
            throw new Exception(string.Format("Type '{0}' is not stored in this ExampleObjectSource.", typeof(T)));
        }

    }
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() {
            return Name;
        }
    }
    public enum Priority { Low, BelowNormal, Normal, AboveNormal, High }
    public static class OutlookDataGenerator {
        static Random rnd = new Random();
        public static readonly User[] Users = new User[] {
            new User() { Id = 0, Name = "Peter Dolan"},
            new User() { Id = 1, Name = "Ryan Fischer"},
            new User() { Id = 2, Name = "Richard Fisher"},
            new User() { Id = 3, Name = "Tom Hamlett" },
            new User() { Id = 4, Name = "Mark Hamilton" },
            new User() { Id = 5, Name = "Steve Lee" },
            new User() { Id = 6, Name = "Jimmy Lewis" },
            new User() { Id = 7, Name = "Jeffrey W McClain" },
            new User() { Id = 8, Name = "Andrew Miller" },
            new User() { Id = 9, Name = "Dave Murrel" },
            new User() { Id = 10, Name = "Bert Parkins" },
            new User() { Id = 11, Name = "Mike Roller" },
            new User() { Id = 12, Name = "Ray Shipman" },
            new User() { Id = 13, Name = "Paul Bailey" },
            new User() { Id = 14, Name = "Brad Barnes" },
            new User() { Id = 15, Name = "Carl Lucas" },
            new User() { Id = 16, Name = "Jerry Campbell" },
        };
        public static string[] Subjects = new string[] { "Integrating Developer Express MasterView control into an Accounting System.",
                                                "Web Edition: Data Entry Page. There is an issue with date validation.",
                                                "Payables Due Calculator is ready for testing.",
                                                "Web Edition: Search Page is ready for testing.",
                                                "Main Menu: Duplicate Items. Somebody has to review all menu items in the system.",
                                                "Receivables Calculator. Where can I find the complete specs?",
                                                "Ledger: Inconsistency. Please fix it.",
                                                "Receivables Printing module is ready for testing.",
                                                "Screen Redraw. Somebody has to look at it.",
                                                "Email System. What library are we going to use?",
                                                "Cannot add new vendor. This module doesn't work!",
                                                "History. Will we track sales history in our system?",
                                                "Main Menu: Add a File menu. File menu item is missing.",
                                                "Currency Mask. The current currency mask in completely unusable.",
                                                "Drag & Drop operations are not available in the scheduler module.",
                                                "Data Import. What database types will we support?",
                                                "Reports. The list of incomplete reports.",
                                                "Data Archiving. We still don't have this features in our application.",
                                                "Email Attachments. Is it possible to add multiple attachments? I haven't found a way to do this.",
                                                "Check Register. We are using different paths for different modules.",
                                                "Data Export. Our customers asked us for export to Microsoft Excel"};

        public static string GetSubject() {
            return Subjects[rnd.Next(Subjects.Length - 1)];
        }

        public static string GetFrom() {
            return Users[GetFromId()].Name;
        }

        public static DateTime GetSentDate() {
            DateTime ret = DateTime.Today;
            int r = rnd.Next(12);
            if (r > 1)
                ret = ret.AddDays(-rnd.Next(50));
            return ret;
        }
        public static int GetSize(bool largeData) {
            return 1000 + (largeData ? 20 * rnd.Next(10000) : 30 * rnd.Next(100));
        }
        public static bool GetHasAttachment() {
            return rnd.Next(10) > 5;
        }
        public static Priority GetPriority() {
            return (Priority)rnd.Next(5);
        }
        public static int GetHoursActive() {
            return (int)Math.Round(rnd.NextDouble() * 1000, 1);
        }
        public static int GetFromId() {
            return rnd.Next(Users.Length);
        }
        public static WpfServerSideGridTest CreateNewObject() {
            WpfServerSideGridTest obj = new WpfServerSideGridTest();
            obj.Subject = OutlookDataGenerator.GetSubject();
            obj.From = OutlookDataGenerator.GetFrom();
            obj.Sent = OutlookDataGenerator.GetSentDate();
            obj.HasAttachment = OutlookDataGenerator.GetHasAttachment();
            obj.Size = OutlookDataGenerator.GetSize(obj.HasAttachment.Value);
            return obj;
        }
    }
}
