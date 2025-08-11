import { useEffect, useMemo, useState } from "react";
import Header from "@/components/Header";
import { Helmet } from "react-helmet-async";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import { Button } from "@/components/ui/button";
import { Slider } from "@/components/ui/slider";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { subjects, findSubject, findTeacher } from "@/data/academics";
import { toast } from "@/hooks/use-toast";

interface Draft {
  rating: number;
  comment: string;
  updatedAt: number;
}

const STUDENT_PROFILE = {
  name: "Jane Doe",
  email: "jane.doe@student.edu",
  className: "10B",
};

const Student = () => {
  const [subjectId, setSubjectId] = useState<string | undefined>();
  const [teacherId, setTeacherId] = useState<string | undefined>();
  const [rating, setRating] = useState<number>(3);
  const [comment, setComment] = useState<string>("");
  const [lastSaved, setLastSaved] = useState<number | null>(null);

  const draftKey = useMemo(() => {
    return subjectId && teacherId ? `draft:${subjectId}:${teacherId}` : undefined;
  }, [subjectId, teacherId]);

  useEffect(() => {
    if (!draftKey) return;
    const raw = localStorage.getItem(draftKey);
    if (raw) {
      const d: Draft = JSON.parse(raw);
      setRating(d.rating);
      setComment(d.comment);
      setLastSaved(d.updatedAt);
    } else {
      setRating(3);
      setComment("");
      setLastSaved(null);
    }
  }, [draftKey]);

  // Autosave on change
  useEffect(() => {
    if (!draftKey) return;
    const handle = setTimeout(() => {
      const payload: Draft = { rating, comment, updatedAt: Date.now() };
      localStorage.setItem(draftKey, JSON.stringify(payload));
      setLastSaved(payload.updatedAt);
    }, 500);
    return () => clearTimeout(handle);
  }, [draftKey, rating, comment]);

  const submit = () => {
    if (!subjectId || !teacherId) {
      toast({ title: "Select subject and teacher", description: "Please pick both before submitting." });
      return;
    }
    const entry = {
      id: crypto.randomUUID(),
      subjectId,
      teacherId,
      rating,
      comment,
      createdAt: Date.now(),
    };
    const list = JSON.parse(localStorage.getItem("feedbacks") || "[]");
    list.unshift(entry);
    localStorage.setItem("feedbacks", JSON.stringify(list));
    toast({ title: "Feedback submitted", description: "Your anonymous feedback was sent." });
    // Clear draft
    const key = `draft:${subjectId}:${teacherId}`;
    localStorage.removeItem(key);
    setLastSaved(null);
    setComment("");
    setRating(3);
  };

  const subj = findSubject(subjectId);
  const teach = findTeacher(subjectId, teacherId);

  return (
    <>
      <Helmet>
        <title>Student Feedback | Class Feedback</title>
        <meta name="description" content="Student feedback form for subjects and teachers. Editable, autosaving drafts." />
        <link rel="canonical" href={typeof window !== 'undefined' ? window.location.href : '/student'} />
      </Helmet>
      <Header role="student" profile={STUDENT_PROFILE} />
      <main className="container mx-auto px-4 py-8">
        <h1 className="text-3xl font-bold mb-6">Student Feedback</h1>
        <section className="grid grid-cols-1 md:grid-cols-3 gap-6">
          <Card className="md:col-span-1 card-elevated">
            <CardHeader>
              <CardTitle>Select Subject & Teacher</CardTitle>
            </CardHeader>
            <CardContent className="space-y-4">
              <div className="space-y-2">
                <Label htmlFor="subject">Subject</Label>
                <Select value={subjectId} onValueChange={(v) => { setSubjectId(v); setTeacherId(undefined); }}>
                  <SelectTrigger id="subject">
                    <SelectValue placeholder="Choose a subject" />
                  </SelectTrigger>
                  <SelectContent>
                    {subjects.map((s) => (
                      <SelectItem key={s.id} value={s.id}>{s.name}</SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
              <div className="space-y-2">
                <Label htmlFor="teacher">Teacher</Label>
                <Select value={teacherId} onValueChange={setTeacherId} disabled={!subjectId}>
                  <SelectTrigger id="teacher">
                    <SelectValue placeholder={subjectId ? "Choose a teacher" : "Select subject first"} />
                  </SelectTrigger>
                  <SelectContent>
                    {subj?.teachers.map((t) => (
                      <SelectItem key={t.id} value={t.id}>{t.name}</SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
            </CardContent>
          </Card>

          <Card className="md:col-span-2 card-elevated">
            <CardHeader>
              <CardTitle>
                {subj && teach ? `${subj.name} — ${teach.name}` : "Feedback Form"}
              </CardTitle>
            </CardHeader>
            <CardContent className="space-y-6">
              <div className="space-y-3">
                <Label>Rating (1 = needs improvement, 5 = excellent)</Label>
                <div className="px-1">
                  <Slider min={1} max={5} step={1} value={[rating]} onValueChange={(v) => setRating(v[0])} />
                </div>
                <div className="text-sm text-muted-foreground">Current: {rating}/5</div>
              </div>

              <div className="space-y-2">
                <Label htmlFor="comment">Your comments</Label>
                <Textarea id="comment" placeholder="Share constructive feedback..." value={comment} onChange={(e) => setComment(e.target.value)} rows={6} />
                <div className="text-xs text-muted-foreground">
                  {lastSaved ? `Autosaved ${new Date(lastSaved).toLocaleTimeString()}` : "Draft will autosave"}
                </div>
              </div>

              <div className="flex items-center gap-3">
                <Button variant="premium" type="button" onClick={() => {
                  if (!draftKey) return;
                  const payload: Draft = { rating, comment, updatedAt: Date.now() };
                  localStorage.setItem(draftKey, JSON.stringify(payload));
                  setLastSaved(payload.updatedAt);
                  toast({ title: "Draft saved" });
                }}>Save draft</Button>
                <Button variant="hero" type="button" onClick={submit}>Submit feedback</Button>
              </div>
            </CardContent>
          </Card>
        </section>
      </main>
    </>
  );
};

export default Student;
