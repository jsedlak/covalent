"use client";

import { createContext, useContext, useEffect, useState, ReactNode } from "react";
import type { ToolProvider } from "@/lib/types/tools";

interface ToolsContextValue {
  toolProviders: ToolProvider[];
  loading: boolean;
  error: string | null;
  refetch: () => Promise<void>;
}

const ToolsContext = createContext<ToolsContextValue | undefined>(undefined);

export function ToolsContextProvider({ children }: { children: ReactNode }) {
  const [toolProviders, setToolProviders] = useState<ToolProvider[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchTools = async () => {
    try {
      setLoading(true);
      setError(null);

      const apiUrl = process.env.NEXT_PUBLIC_API_URL || process.env.NEXT_PUBLIC_SERVICES__COVALENT_SILO__HTTP__0;
      if (!apiUrl) {
        throw new Error("API URL not configured");
      }

      const response = await fetch(`${apiUrl}/api/tools`);

      if (!response.ok) {
        throw new Error(
          `Failed to fetch tools: ${response.status} ${response.statusText}`
        );
      }

      const data: ToolProvider[] = await response.json();
      setToolProviders(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : "An error occurred");
      setToolProviders([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchTools();
  }, []);

  return (
    <ToolsContext.Provider
      value={{
        toolProviders,
        loading,
        error,
        refetch: fetchTools,
      }}
    >
      {children}
    </ToolsContext.Provider>
  );
}

export function useTools() {
  const context = useContext(ToolsContext);
  if (context === undefined) {
    throw new Error("useTools must be used within a ToolsContextProvider");
  }
  return context;
}

