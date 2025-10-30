"use client";

import React, { useState } from "react";
import { ChevronDown, ChevronRight } from "lucide-react";
import { useTools } from "@/lib/contexts/tools-context";
import { Badge } from "@/components/ui/badge";
import { Tooltip, TooltipContent, TooltipTrigger } from "@/components/ui/tooltip";
import { Skeleton } from "@/components/ui/skeleton";

export default function ToolsPage() {
  const { toolProviders, loading, error } = useTools();
  const [expandedRows, setExpandedRows] = useState<Set<string>>(new Set());

  const toggleRow = (providerId: string) => {
    setExpandedRows((prev) => {
      const next = new Set(prev);
      if (next.has(providerId)) {
        next.delete(providerId);
      } else {
        next.add(providerId);
      }
      return next;
    });
  };

  if (loading) {
    return (
      <div className="flex flex-1 flex-col gap-4 p-4 pt-0">
        <div className="min-h-[100vh] flex-1 rounded-xl bg-muted/50 md:min-h-min">
          <div className="p-6">
            <h1 className="text-3xl font-bold tracking-tight mb-6">Tools</h1>
            <div className="space-y-4">
              <Skeleton className="h-12 w-full" />
              <Skeleton className="h-12 w-full" />
              <Skeleton className="h-12 w-full" />
            </div>
          </div>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="flex flex-1 flex-col gap-4 p-4 pt-0">
        <div className="min-h-[100vh] flex-1 rounded-xl bg-muted/50 md:min-h-min">
          <div className="p-6">
            <h1 className="text-3xl font-bold tracking-tight mb-6">Tools</h1>
            <div className="bg-red-50 border border-red-200 rounded-lg p-4">
              <div className="text-red-800 font-medium">Error</div>
              <div className="text-red-600 text-sm mt-1">{error}</div>
            </div>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="flex flex-1 flex-col gap-4 p-4 pt-0">
      <div className="min-h-[100vh] flex-1 rounded-xl bg-muted/50 md:min-h-min">
        <div className="p-6">
          <h1 className="text-3xl font-bold tracking-tight mb-6">Tools</h1>

          {toolProviders.length === 0 ? (
            <div className="text-center py-8 text-muted-foreground">
              No tool providers found
            </div>
          ) : (
            <div className="border rounded-lg overflow-hidden">
              <table className="w-full">
                <thead className="bg-muted/50 border-b">
                  <tr>
                    <th className="w-12 px-4 py-3 text-left"></th>
                    <th className="px-4 py-3 text-left font-semibold">Provider</th>
                    <th className="px-4 py-3 text-left font-semibold">Description</th>
                    <th className="px-4 py-3 text-left font-semibold">Tools</th>
                  </tr>
                </thead>
                <tbody>
                  {toolProviders.map((provider) => {
                    const isExpanded = expandedRows.has(provider.id);
                    return (
                      <React.Fragment key={provider.id}>
                        <tr
                          className="border-b hover:bg-muted/30 cursor-pointer transition-colors"
                          onClick={() => toggleRow(provider.id)}
                        >
                          <td className="px-4 py-4">
                            {isExpanded ? (
                              <ChevronDown className="h-4 w-4 text-muted-foreground" />
                            ) : (
                              <ChevronRight className="h-4 w-4 text-muted-foreground" />
                            )}
                          </td>
                          <td className="px-4 py-4 font-medium">{provider.name}</td>
                          <td className="px-4 py-4 text-muted-foreground">
                            {provider.description}
                          </td>
                          <td className="px-4 py-4">
                            <span className="text-sm text-muted-foreground">
                              {provider.tools.length} tool{provider.tools.length !== 1 ? "s" : ""}
                            </span>
                          </td>
                        </tr>
                        {isExpanded && (
                          <tr>
                            <td colSpan={4} className="px-4 py-4 bg-muted/20">
                              <div className="flex flex-wrap gap-2">
                                {provider.tools.length === 0 ? (
                                  <span className="text-sm text-muted-foreground">
                                    No tools available
                                  </span>
                                ) : (
                                  provider.tools.map((tool) => (
                                    <Tooltip key={tool.id}>
                                      <TooltipTrigger asChild>
                                        <div>
                                          <Badge variant="secondary" className="cursor-help">
                                            {tool.id}
                                          </Badge>
                                        </div>
                                      </TooltipTrigger>
                                      <TooltipContent>
                                        <div className="max-w-xs">
                                          <div className="font-semibold mb-1">{tool.name}</div>
                                          <div className="text-xs opacity-90">{tool.description}</div>
                                          <div className="text-xs opacity-70 mt-1 pt-1 border-t border-white/20">
                                            {tool.typeName}.{tool.methodName}
                                          </div>
                                        </div>
                                      </TooltipContent>
                                    </Tooltip>
                                  ))
                                )}
                              </div>
                            </td>
                          </tr>
                        )}
                      </React.Fragment>
                    );
                  })}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
