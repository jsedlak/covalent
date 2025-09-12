"use client"

import { useSidebar as useShadcnSidebar } from "@/components/ui/sidebar"

export function useSidebar() {
  return useShadcnSidebar()
}

export type SidebarState = "expanded" | "collapsed"

export interface SidebarHook {
  state: SidebarState
  open: boolean
  setOpen: (open: boolean) => void
  openMobile: boolean
  setOpenMobile: (open: boolean) => void
  isMobile: boolean
  toggleSidebar: () => void
}