import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Table, TableBody, TableCaption, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";

// Temporary mock data until backend is connected
// All reviews are anonymous and represent individual submissions
const reviews = [
  { id: "1", subject: "Mathematics", q1: 4, q2: 5, comments: "Explains really clearly." },
  { id: "2", subject: "Physics", q1: 3, q2: 4, comments: "Pace is a bit fast but engaging." },
  { id: "3", subject: "Chemistry", q1: 5, q2: 5, comments: "Great examples and structure." },
  { id: "4", subject: "Mathematics", q1: 4, q2: 4, comments: "Would love more practice problems." },
];

export default function TeacherDashboard() {
  return (
    <main className="container mx-auto px-6 py-10">
      <header className="mb-8">
        <h1 className="text-3xl font-bold">Teacher Reviews</h1>
        <p className="text-muted-foreground">All reviews below are anonymous and limited to your own subjects.</p>
      </header>

      <section>
        <Card>
          <CardHeader>
            <CardTitle>Anonymous Reviews</CardTitle>
          </CardHeader>
          <CardContent>
            <Table>
              <TableCaption>Individual, anonymous feedback entries.</TableCaption>
              <TableHeader>
                <TableRow>
                  <TableHead>Subject</TableHead>
                  <TableHead>Q1: Clarity</TableHead>
                  <TableHead>Q2: Engagement</TableHead>
                  <TableHead>Comments</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {reviews.map((r) => (
                  <TableRow key={r.id}>
                    <TableCell>{r.subject}</TableCell>
                    <TableCell>{r.q1}</TableCell>
                    <TableCell>{r.q2}</TableCell>
                    <TableCell className="min-w-[220px]">{r.comments || "—"}</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </CardContent>
        </Card>
      </section>
    </main>
  );
}
