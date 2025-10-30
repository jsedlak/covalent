export interface Tool {
  id: string;
  name: string;
  description: string;
  typeName: string;
  methodName: string;
  slug: string;
}

export interface ToolProvider {
  id: string;
  name: string;
  description: string;
  tools: Tool[];
}

