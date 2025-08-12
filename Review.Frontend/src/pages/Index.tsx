import { Button } from "@/components/ui/button";
import { LogIn } from "lucide-react";
import { toast } from "sonner";

const Index = () => {
  const handleLogin = () => {
    toast("Connect Supabase to enable Google OAuth restricted to gimi.ro.");
  };

  return (
    <div className="min-h-screen relative overflow-hidden bg-background">
      <div className="pointer-events-none absolute inset-0 opacity-40">
        <div className="absolute -top-24 -left-24 h-96 w-96 rounded-full blur-3xl bg-gradient-to-r from-[hsl(var(--primary))] to-[hsl(var(--primary-glow))]" />
        <div className="absolute -bottom-24 -right-24 h-96 w-96 rounded-full blur-3xl bg-gradient-to-r from-[hsl(var(--primary-glow))] to-[hsl(var(--primary))]" />
      </div>

      <main className="container mx-auto px-6 py-24 text-center relative">
        <h1 className="text-4xl md:text-5xl font-bold tracking-tight mb-4">
          Anonymous Student Feedback – GIMI
        </h1>
        <p className="text-lg md:text-xl text-muted-foreground max-w-2xl mx-auto mb-10">
          Secure, role-based platform for collecting and analyzing student feedback. Built for teachers, admins, and management.
        </p>
        <div className="flex items-center justify-center gap-4">
          <Button size="lg" variant="hero" onClick={handleLogin} aria-label="Continue with Google (gimi.ro)">
            <LogIn /> Continue with Google (gimi.ro)
          </Button>
        </div>

        <section className="mt-14 grid gap-6 md:grid-cols-3 text-left">
          <article className="rounded-lg border bg-card p-6 shadow-sm">
            <h2 className="text-xl font-semibold mb-2">Students</h2>
            <p className="text-muted-foreground">Submit anonymous feedback per subject and teacher. Edit until submitted.</p>
          </article>
          <article className="rounded-lg border bg-card p-6 shadow-sm">
            <h2 className="text-xl font-semibold mb-2">Teachers</h2>
            <p className="text-muted-foreground">View aggregated stats for your own subjects. Privacy by design.</p>
          </article>
          <article className="rounded-lg border bg-card p-6 shadow-sm">
            <h2 className="text-xl font-semibold mb-2">Admin & HoD</h2>
            <p className="text-muted-foreground">Control windows, eligible grades, and exports to Excel. School-wide insights.</p>
          </article>
        </section>

        <nav className="mt-10 text-sm text-muted-foreground">
          <span className="block mb-2">Preview dashboards:</span>
          <div className="flex flex-wrap items-center justify-center gap-3">
            <a className="underline hover:text-foreground" href="/dashboard/student">Student</a>
            <a className="underline hover:text-foreground" href="/dashboard/teacher">Teacher</a>
            <a className="underline hover:text-foreground" href="/dashboard/admin">Admin</a>
            <a className="underline hover:text-foreground" href="/dashboard/hod">HoD</a>
          </div>
        </nav>
      </main>
    </div>
  );
};

export default Index;
