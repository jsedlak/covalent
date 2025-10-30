import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
  env: {
    API_URL: process.env["services__covalent-silo__http__0"],
    NEXT_PUBLIC_API_URL: process.env["services__covalent-silo__http__0"],
    NEXT_PUBLIC_SERVICES__COVALENT_SILO__HTTP__0:
      process.env["services__covalent-silo__http__0"],
  },
};

export default nextConfig;
