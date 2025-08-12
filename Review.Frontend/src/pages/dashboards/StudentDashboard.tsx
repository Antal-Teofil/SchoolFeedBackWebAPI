import { FeedbackForm } from "@/components/feedback/FeedbackForm";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";

const subjects = [
  { id: "math", name: "Mathematics" },
  { id: "phy", name: "Physics" },
  { id: "eng", name: "English" },
];
const teachers = [
  { id: "t1", name: "Prof. Ionescu" },
  { id: "t2", name: "Mrs. Popescu" },
  { id: "t3", name: "Mr. Marinescu" },
];

export default function StudentDashboard() {
  return (
    <main className="container mx-auto px-6 py-10">
      <header className="mb-8">
        <h1 className="text-3xl font-bold">Student Dashboard</h1>
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
        <FeedbackForm subjects={subjects} teachers={teachers} />
      </section>
    </main>
  );
}
