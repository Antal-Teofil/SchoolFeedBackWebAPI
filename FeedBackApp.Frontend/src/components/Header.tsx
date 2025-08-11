import { Link, useLocation } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";
import { Sheet, SheetContent, SheetHeader, SheetTitle, SheetTrigger } from "@/components/ui/sheet";
import { Menu } from "lucide-react";

interface ProfileInfo {
  name: string;
  email: string;
  className?: string;
}

interface HeaderProps {
  role: "student" | "teacher" | "guest";
  profile?: ProfileInfo;
}

const Header = ({ role, profile }: HeaderProps) => {
  const location = useLocation();
  const isActive = (path: string) => location.pathname === path;

  return (
    <header className="w-full border-b bg-background/80 backdrop-blur supports-[backdrop-filter]:bg-background/60 sticky top-0 z-40">
      <div className="container mx-auto px-4 py-3 flex items-center justify-between">
        <nav className="flex items-center gap-2">
          <Link to="/" className="text-base font-semibold tracking-tight">
            Class Feedback
          </Link>

          {/* Mobile menu */}
          <div className="sm:hidden ml-2">
            <Sheet>
              <SheetTrigger asChild>
                <Button variant="ghost" size="icon" aria-label="Open menu">
                  <Menu className="h-5 w-5" />
                </Button>
              </SheetTrigger>
              <SheetContent side="left">
                <SheetHeader>
                  <SheetTitle>Navigation</SheetTitle>
                </SheetHeader>
                <nav className="mt-4 grid gap-2">
                  <Link to="/student">
                    <Button variant={isActive("/student") ? "secondary" : "ghost"} className="justify-start w-full">
                      Student
                    </Button>
                  </Link>
                  <Link to="/teacher">
                    <Button variant={isActive("/teacher") ? "secondary" : "ghost"} className="justify-start w-full">
                      Teacher
                    </Button>
                  </Link>
                </nav>
              </SheetContent>
            </Sheet>
          </div>

          {/* Desktop nav */}
          <div className="hidden sm:flex items-center gap-1 ml-4">
            <Link to="/student">
              <Button variant={isActive("/student") ? "secondary" : "ghost"} size="sm">
                Student
              </Button>
            </Link>
            <Link to="/teacher">
              <Button variant={isActive("/teacher") ? "secondary" : "ghost"} size="sm">
                Teacher
              </Button>
            </Link>
          </div>
        </nav>

        <div className="flex items-center gap-3 text-sm">
          {profile ? (
            <div className="text-right leading-tight">
              <div className="font-medium truncate max-w-[140px] sm:max-w-none">{profile.name}</div>
              <div className="text-muted-foreground hidden sm:block">{profile.email}</div>
              {profile.className && (
                <div className="text-muted-foreground text-xs hidden sm:block">Class: {profile.className}</div>
              )}
            </div>
          ) : (
            <div className="text-muted-foreground">{role === "guest" ? "Guest" : role}</div>
          )}
        </div>
      </div>
    </header>
  );
};

export default Header;
