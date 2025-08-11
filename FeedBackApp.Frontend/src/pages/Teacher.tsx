import { useEffect, useMemo, useState } from "react";
import Header from "@/components/Header";
import { Helmet } from "react-helmet-async";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import { subjects, findSubject, findTeacher } from "@/data/academics";
import * as XLSX from "xlsx";

interface FeedbackEntry {
  id: string;
  subjectId: string;
  teacherId: string;
  rating: number;
  comment: string;
  createdAt: number;
}

const TEACHER_PROFILE = {
  name: "Mr. Instructor",
  email: "teacher@school.edu",
};

function groupStats(rows: FeedbackEntry[]) {
  const map = new Map<string, { subject: string; teacher: string; count: number; total: number }>();
  for (const r of rows) {
    const subj = findSubject(r.subjectId)?.name || r.subjectId;
    const teach = findTeacher(r.subjectId, r.teacherId)?.name || r.teacherId;
    const key = `${r.subjectId}:${r.teacherId}`;
    if (!map.has(key)) {
      map.set(key, { subject: subj, teacher: teach, count: 0, total: 0 });
    }
    const item = map.get(key)!;
    item.count += 1;
    item.total += r.rating;
  }
  return Array.from(map.values()).map((v) => ({
    subject: v.subject,
    teacher: v.teacher,
    count: v.count,
    averageRating: Number((v.total / v.count).toFixed(2)),
  }));
}

const Teacher = () => {
  const [rows, setRows] = useState<FeedbackEntry[]>([]);

  useEffect(() => {
    const data = JSON.parse(localStorage.getItem("feedbacks") || "[]");
    setRows(data);
  }, []);

  const stats = useMemo(() => groupStats(rows), [rows]);

  const exportExcel = () => {
    const feedbackSheet = rows.map((r) => ({
      Date: new Date(r.createdAt).toLocaleString(),
      Subject: findSubject(r.subjectId)?.name || r.subjectId,
      Teacher: findTeacher(r.subjectId, r.teacherId)?.name || r.teacherId,
      Rating: r.rating,
      Comment: r.comment,
    }));

    const statsSheet = stats.map((s) => ({
      Subject: s.subject,
      Teacher: s.teacher,
      Count: s.count,
      AverageRating: s.averageRating,
    }));

    const wb = XLSX.utils.book_new();
    const ws1 = XLSX.utils.json_to_sheet(feedbackSheet);
    const ws2 = XLSX.utils.json_to_sheet(statsSheet);
    XLSX.utils.book_append_sheet(wb, ws1, "Feedback");
    XLSX.utils.book_append_sheet(wb, ws2, "Statistics");
    XLSX.writeFile(wb, `feedback_statistics_${new Date().toISOString().slice(0,10)}.xlsx`);
  };

  return (
    <>
      <Helmet>
        <title>Teacher Dashboard | Class Feedback</title>
        <meta name="description" content="Teacher dashboard to view anonymous feedback and export global statistics to Excel." />
        <link rel="canonical" href={typeof window !== 'undefined' ? window.location.href : '/teacher'} />
      </Helmet>
      <Header role="teacher" profile={TEACHER_PROFILE} />
      <main className="container mx-auto px-4 py-8 space-y-8">
        <div className="flex items-center justify-between">
          <h1 className="text-3xl font-bold">Teacher Dashboard</h1>
          <Button variant="hero" onClick={exportExcel}>Export Excel</Button>
        </div>

        <section className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          <Card className="lg:col-span-1 card-elevated">
            <CardHeader>
              <CardTitle>Global Statistics</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="w-full overflow-x-auto">
                <Table>
                  <TableHeader>
                    <TableRow>
                      <TableHead>Subject</TableHead>
                      <TableHead>Teacher</TableHead>
                      <TableHead className="text-right">Avg</TableHead>
                    </TableRow>
                  </TableHeader>
                  <TableBody>
                    {stats.map((s) => (
                      <TableRow key={`${s.subject}-${s.teacher}`}>
                        <TableCell>{s.subject}</TableCell>
                        <TableCell>{s.teacher}</TableCell>
                        <TableCell className="text-right">{s.averageRating}</TableCell>
                      </TableRow>
                    ))}
                    {stats.length === 0 && (
                      <TableRow>
                        <TableCell colSpan={3} className="text-center text-muted-foreground">No feedback yet.</TableCell>
                      </TableRow>
                    )}
                  </TableBody>
                </Table>
              </div>
            </CardContent>
          </Card>

          <Card className="lg:col-span-2 card-elevated">
            <CardHeader>
              <CardTitle>All Feedback (Anonymous)</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="w-full overflow-x-auto">
                <Table>
                  <TableHeader>
                    <TableRow>
                      <TableHead>Date</TableHead>
                      <TableHead>Subject</TableHead>
                      <TableHead>Teacher</TableHead>
                      <TableHead>Rating</TableHead>
                      <TableHead>Comment</TableHead>
                    </TableRow>
                  </TableHeader>
                  <TableBody>
                    {rows.map((r) => (
                      <TableRow key={r.id}>
                        <TableCell>{new Date(r.createdAt).toLocaleString()}</TableCell>
                        <TableCell>{findSubject(r.subjectId)?.name || r.subjectId}</TableCell>
                        <TableCell>{findTeacher(r.subjectId, r.teacherId)?.name || r.teacherId}</TableCell>
                        <TableCell>{r.rating}</TableCell>
                        <TableCell className="max-w-[320px] truncate" title={r.comment}>{r.comment}</TableCell>
                      </TableRow>
                    ))}
                    {rows.length === 0 && (
                      <TableRow>
                        <TableCell colSpan={5} className="text-center text-muted-foreground">No feedback found.</TableCell>
                      </TableRow>
                    )}
                  </TableBody>
                </Table>
              </div>
            </CardContent>
          </Card>
        </section>
      </main>
    </>
  );
};

export default Teacher;
