import { StudentContext } from "@/models/StudentContext";

export const mockStudentContext: StudentContext = {
  grade: "11C",
  subjects: ["Mathematics", "Physics", "English"],
  teachers: ["Prof. Ionescu", "Mrs. Popescu", "Mr. Marinescu"],
  evaluations: [
    {
      id: "eval-1",
      subject: "Mathematics",
      teacher: "Prof. Ionescu",
      status: "Draft",
      responses: { q1: "5", q2: "4", q3: "3", q20: ["1", "3"], q21: ["2"] }
    },
    {
      id: "eval-2",
      subject: "Physics",
      teacher: "Mrs. Popescu",
      status: "Submitted",
      responses: {
        q1: "4",
        q2: "4",
        q3: "5",
        q22: "Nagyon érdekesek voltak az órák, sok új dolgot tanultam.",
        q23: "Néha túl gyors volt a tempó."
      }
    }
  ]
};
