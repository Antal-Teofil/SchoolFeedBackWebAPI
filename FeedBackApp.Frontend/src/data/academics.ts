export type Teacher = { id: string; name: string };
export type Subject = { id: string; name: string; teachers: Teacher[] };

export const subjects: Subject[] = [
  { id: "math", name: "Mathematics", teachers: [
    { id: "t-smith", name: "Mr. Smith" },
    { id: "t-lee", name: "Ms. Lee" },
  ]},
  { id: "eng", name: "English", teachers: [
    { id: "t-johnson", name: "Mrs. Johnson" },
    { id: "t-brown", name: "Mr. Brown" },
  ]},
  { id: "sci", name: "Science", teachers: [
    { id: "t-davis", name: "Dr. Davis" },
    { id: "t-clark", name: "Ms. Clark" },
  ]},
];

export const findSubject = (id?: string) => subjects.find(s => s.id === id);
export const findTeacher = (subjectId?: string, teacherId?: string) =>
  findSubject(subjectId)?.teachers.find(t => t.id === teacherId);
