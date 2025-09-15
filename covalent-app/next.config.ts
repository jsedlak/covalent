import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
  env: {
    NEXT_PUBLIC_SERVICES__COVALENT_SILO__HTTP__0:
      process.env["services__covalent-silo__http__0"],
  },
};

export default nextConfig;
