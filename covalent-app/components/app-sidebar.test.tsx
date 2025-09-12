import { render, screen } from '@testing-library/react'
import { AppSidebar } from './app-sidebar'

// Mock Next.js navigation hooks
jest.mock('next/navigation', () => ({
  usePathname: jest.fn(() => '/'),
}))

// Mock the sidebar hook
jest.mock('@/lib/hooks/use-sidebar', () => ({
  useSidebar: jest.fn(() => ({
    state: 'expanded',
    toggleSidebar: jest.fn(),
  })),
}))

// Mock the shadcn sidebar components
jest.mock('@/components/ui/sidebar', () => ({
  Sidebar: ({ children }: { children: React.ReactNode }) => <div data-testid="sidebar">{children}</div>,
  SidebarContent: ({ children }: { children: React.ReactNode }) => <div>{children}</div>,
  SidebarFooter: ({ children }: { children: React.ReactNode }) => <div>{children}</div>,
  SidebarGroup: ({ children }: { children: React.ReactNode }) => <div>{children}</div>,
  SidebarGroupContent: ({ children }: { children: React.ReactNode }) => <div>{children}</div>,
  SidebarHeader: ({ children }: { children: React.ReactNode }) => <div>{children}</div>,
  SidebarMenu: ({ children }: { children: React.ReactNode }) => <ul>{children}</ul>,
  SidebarMenuButton: ({ children, asChild }: { children: React.ReactNode; asChild?: boolean }) => 
    asChild ? <>{children}</> : <button>{children}</button>,
  SidebarMenuItem: ({ children }: { children: React.ReactNode }) => <li>{children}</li>,
  SidebarTrigger: ({ children }: { children: React.ReactNode }) => <button>{children}</button>,
}))

// Mock the button component
jest.mock('@/components/ui/button', () => ({
  Button: ({ children, onClick }: { children: React.ReactNode; onClick?: () => void }) => 
    <button onClick={onClick}>{children}</button>,
}))

describe('AppSidebar', () => {
  it('renders the sidebar with Covalent branding', () => {
    render(<AppSidebar />)
    
    expect(screen.getByTestId('sidebar')).toBeInTheDocument()
    expect(screen.getByText('Covalent')).toBeInTheDocument()
  })

  it('renders all navigation items', () => {
    render(<AppSidebar />)
    
    expect(screen.getByText('Dashboard')).toBeInTheDocument()
    expect(screen.getByText('Deployments')).toBeInTheDocument()
    expect(screen.getByText('Agents')).toBeInTheDocument()
    expect(screen.getByText('Tools')).toBeInTheDocument()
    expect(screen.getByText('Providers')).toBeInTheDocument()
    expect(screen.getByText('Search')).toBeInTheDocument()
    expect(screen.getByText('Settings')).toBeInTheDocument()
  })

  it('renders collapsed state correctly', () => {
    const mockUseSidebar = require('@/lib/hooks/use-sidebar').useSidebar
    mockUseSidebar.mockReturnValue({
      state: 'collapsed',
      toggleSidebar: jest.fn(),
    })
    
    render(<AppSidebar />)
    
    // Should show 'C' instead of 'Covalent' when collapsed
    expect(screen.getByText('C')).toBeInTheDocument()
    expect(screen.queryByText('Covalent')).not.toBeInTheDocument()
  })
})