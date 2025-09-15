"use client";

import { useEffect, useState } from "react";

type Provider = {
  name: string;
  category: string;
};

export default function ProvidersPage() {
  const [providers, setProviders] = useState<Provider[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchProviders = async () => {
      try {
        setLoading(true);
        setError(null);

        const baseUrl =
          process.env.NEXT_PUBLIC_SERVICES__COVALENT_SILO__HTTP__0;
        if (!baseUrl) {
          throw new Error("API base URL not configured");
        }

        const response = await fetch(`${baseUrl}/api/providers`);

        if (!response.ok) {
          throw new Error(
            `Failed to fetch providers: ${response.status} ${response.statusText}`
          );
        }

        const data = await response.json();
        setProviders(data);
      } catch (err) {
        setError(err instanceof Error ? err.message : "An error occurred");
      } finally {
        setLoading(false);
      }
    };

    fetchProviders();
  }, []);

  return (
    <div className="flex flex-1 flex-col gap-4 p-4 pt-0">
      <div className="min-h-[100vh] flex-1 rounded-xl bg-muted/50 md:min-h-min">
        <div className="p-6">
          <h1 className="text-3xl font-bold tracking-tight mb-6">Providers</h1>

          {loading && (
            <div className="flex items-center justify-center py-8">
              <div className="text-muted-foreground">Loading providers...</div>
            </div>
          )}

          {error && (
            <div className="bg-red-50 border border-red-200 rounded-lg p-4 mb-6">
              <div className="text-red-800 font-medium">Error</div>
              <div className="text-red-600 text-sm mt-1">{error}</div>
            </div>
          )}

          {!loading && !error && (
            <div className="space-y-4">
              {providers.length === 0 ? (
                <div className="text-center py-8 text-muted-foreground">
                  No providers found
                </div>
              ) : (
                <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
                  {providers.map((provider, index) => (
                    <div
                      key={index}
                      className="bg-background border rounded-lg p-4 hover:shadow-md transition-shadow"
                    >
                      <h3 className="font-semibold text-lg mb-2">
                        {provider.name}
                      </h3>
                      <p className="text-muted-foreground text-sm">
                        Category: {provider.category}
                      </p>
                    </div>
                  ))}
                </div>
              )}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
