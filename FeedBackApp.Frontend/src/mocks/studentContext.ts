import { StudentContext } from "@/models/StudentContext";

export const mockStudentContext: StudentContext = {
  grade: "11C",
  subjects: ["Mathematics", "Physics", "English","Magyar"],
  teachers: ["Prof. Ionescu", "Mrs. Popescu", "Mr. Marinescu","Teofil"],
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
        q2: "5",
        q3: "4",
        q4: "4",
        q5: "4",
        q6: "4",
        q7: "5",
        q8: "4",
        q9: "3",
        q10: "4",
        q11: "4",
        q12: "5",
        q13: "3", 
        q14: "4",
        q15: "3",
        q16: "5",
        q17: "2",
        q18: "2",          
        q19: "2",             
        q20: ["1", "3"],      
        q21: ["1", "4"],     
        q22: "Az órákon sok kísérletet láttunk és mérési feladatokat oldottunk meg. A magyarázatok érthetőek voltak, és a példák segítettek összekapcsolni az elméletet a gyakorlattal.",
        q23: "Időnként túl gyors volt a tempó, emiatt néhány fontosabb lépésen gyorsan átsiklottunk. Jó lenne több rövid összefoglaló a részek végén és lassabb magyarázat a nehezebb témáknál.",
        q24: "4", 
        q25: "4", 
        q26: "1" 
      }
    },
    {
      id: "eval-3",
      subject: "Magyar",
      teacher: "Teofil",
      status: "Submitted",
      responses: {
        q1: "4",
        q2: "5",
        q3: "4",
        q4: "4",
        q5: "4",
        q6: "4",
        q7: "5",
        q8: "4",
        q9: "3",
        q10: "4",
        q11: "4",
        q12: "5",
        q13: "3", 
        q14: "4",
        q15: "3",
        q16: "5",
        q17: "2",
        q18: "2",          
        q19: "2",             
        q20: ["1", "3"],      
        q21: ["1", "4"],     
        q22: "Az órákon sok kísérletet láttunk és mérési feladatokat oldottunk meg. A magyarázatok érthetőek voltak, és a példák segítettek összekapcsolni az elméletet a gyakorlattal.",
        q23: "Időnként túl gyors volt a tempó, emiatt néhány fontosabb lépésen gyorsan átsiklottunk. Jó lenne több rövid összefoglaló a részek végén és lassabb magyarázat a nehezebb témáknál.",
        q24: "4", 
        q25: "4", 
        q26: "1" 
      }
    }

  ]
};
