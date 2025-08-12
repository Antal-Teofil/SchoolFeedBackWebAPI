import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Table, TableBody, TableCaption, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { useReviews } from "../../hooks/useReviews"

// Temporary mock data until backend is connected
// All reviews are anonymous and represent individual submissions
const reviews = [
  { id: "1", subject: "Mathematics", q1: 4, q2: 5,q3: 4, q4: 5,q5: 4, q6: 5,q7: 4, q8: 5,q9: 4, q10: 5,q11: 4,
     q12: 5,q13: 4, q14: 5,q15: 4, q16: 5,q17: 4, q18: 5,q19: 4, q20: 5,q21: 4, q22: 5,q23: 4, q24: 5,q25: 4, q26: 5 },
];


export default function TeacherDashboard() {

/*  const {
    allReviews,
    isLoadingAllReviews,
    isErrorAllReviews,
    errorAllReview,
  } = useReviews();

  if (isLoadingAllReviews) {
    return <div>Loading Reviews.....</div>
  }
  if (isErrorAllReviews) {
    return <div className="error">Error has occured:{errorAllReview.message}</div>
  }*/

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
                  <TableHead>Q1</TableHead>
                  <TableHead>Q2</TableHead>
                  <TableHead>Q3</TableHead>
                  <TableHead>Q4</TableHead>
                  <TableHead>Q5</TableHead>
                  <TableHead>Q6</TableHead>
                  <TableHead>Q7</TableHead>
                  <TableHead>Q8</TableHead>
                  <TableHead>Q9</TableHead>
                  <TableHead>Q10</TableHead>
                  <TableHead>Q11</TableHead>
                  <TableHead>Q12</TableHead>
                  <TableHead>Q13</TableHead>
                  <TableHead>Q14</TableHead>
                  <TableHead>Q15</TableHead>
                  <TableHead>Q16</TableHead>
                  <TableHead>Q17</TableHead>
                  <TableHead>Q18</TableHead>
                  <TableHead>Q19</TableHead>
                  <TableHead>Q20</TableHead>
                  <TableHead>Q21</TableHead>
                  <TableHead>Q22</TableHead>
                  <TableHead>Q23</TableHead>
                  <TableHead>Q24</TableHead>
                  <TableHead>Q25</TableHead>
                  <TableHead>Q26</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {reviews.map((r) => (
                  <TableRow key={r.id}>
                    <TableCell>{r.subject}</TableCell>
                    <TableCell>{r.q1}</TableCell>
                    <TableCell>{r.q2}</TableCell>
                    <TableCell>{r.q3}</TableCell>
                    <TableCell>{r.q4}</TableCell>
                    <TableCell>{r.q5}</TableCell>
                    <TableCell>{r.q6}</TableCell>
                    <TableCell>{r.q7}</TableCell>
                    <TableCell>{r.q8}</TableCell>
                    <TableCell>{r.q9}</TableCell>
                    <TableCell>{r.q10}</TableCell>
                    <TableCell>{r.q11}</TableCell>
                    <TableCell>{r.q12}</TableCell>
                    <TableCell>{r.q13}</TableCell>
                    <TableCell>{r.q14}</TableCell>
                    <TableCell>{r.q15}</TableCell>
                    <TableCell>{r.q16}</TableCell>
                    <TableCell>{r.q17}</TableCell>
                    <TableCell>{r.q18}</TableCell>
                    <TableCell>{r.q19}</TableCell>
                    <TableCell>{r.q20}</TableCell>
                    <TableCell>{r.q21}</TableCell>
                    <TableCell>{r.q22}</TableCell>
                    <TableCell>{r.q23}</TableCell>
                    <TableCell>{r.q24}</TableCell>
                    <TableCell>{r.q25}</TableCell>
                    <TableCell>{r.q26}</TableCell>
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
