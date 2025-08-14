import { FeedbackForm } from "@/components/feedback/FeedbackForm";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { useReviews } from "../../hooks/useReviews";
import { toast } from "sonner";
import { useAuthStore } from '@/stores/useAuthStore'
import { useEffect, useState } from "react";


/*
const subjects = ["Mathematics", "Physics","English"];
const teachers = ["Prof. Ionescu","Mrs. Popescu","Mr. Marinescu"];*/

type StudentContext = {
  grade: string
  teachers : string[],
  subjects : string[],
}

export default function StudentDashboard() {
  const user = useAuthStore((state) => state.user);
  const [context,setContext] =useState<StudentContext | null>(null);

  /*const {
  getStudentByEmail,
  isGettingStudent,
  } = useReviews();

  useEffect(() => {
    if (!user?.email) return;

    getStudentByEmail(user.email, {
      onSuccess: (data: StudentContext) => {
        setContext(data);
      },
      onError: (err) => {
        console.error(err);
      },
    });
    if(isGettingStudent)
    {
      toast("IsLoading StudentContext");
    }
  }, [user?.email, getStudentByEmail]);
  */
 useEffect(() => {
  setContext({
    grade: "11C",
    teachers: ["Prof. Ionescu", "Mrs. Popescu"],
    subjects: ["Mathematics", "Physics", "English"],
  });
}, []);

   if (!context) {
    return (
      <main className="container mx-auto px-6 py-10">
        <h1 className="text-2xl">Loading student context…</h1>
      </main>
    );
  }

  return (
    <main className="container mx-auto px-6 py-10">
      <header className="mb-8">
        <h1 className="text-3xl font-bold">Welcome,{user.firstName} grade={context.grade}</h1>
        <p className="text-muted-foreground">Submit feedback for your enrolled subjects.</p>
      </header>

      <section className="grid gap-6 md:grid-cols-3 mb-10">
        <Card>
          <CardHeader>
            <CardTitle>Mathematics</CardTitle>
          </CardHeader>
          <CardContent className="text-muted-foreground">Status: Not started</CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>Physics</CardTitle>
          </CardHeader>
          <CardContent className="text-muted-foreground">Status: In progress</CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>English</CardTitle>
          </CardHeader>
          <CardContent className="text-muted-foreground">Status: Submitted</CardContent>
        </Card>
      </section>

      <section>
        <FeedbackForm subjects={context.subjects} teachers={context.teachers} />
      </section>
    </main>
  );
}
