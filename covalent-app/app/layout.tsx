import type { Metadata } from "next";
import { Rubik } from "next/font/google";
import "./globals.css";
import { SidebarLayout } from "@/components/sidebar-layout";
import { ToolsContextProvider } from "@/lib/contexts/tools-context";

const rubik = Rubik({
  variable: "--font-rubik",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Covalent",
  description: "Covalent Application",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body
        className={`${rubik.variable} antialiased font-sans`}
      >
        <ToolsContextProvider>
          <SidebarLayout>
            {children}
          </SidebarLayout>
        </ToolsContextProvider>
      </body>
    </html>
  );
}
