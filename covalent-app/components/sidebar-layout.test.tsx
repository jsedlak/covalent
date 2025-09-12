import { render, screen } from '@testing-library/react'
import { SidebarLayout } from './sidebar-layout'

// Mock the shadcn sidebar components
jest.mock('@/components/ui/sidebar', () => ({
  SidebarProvider: ({ children }: { children: React.ReactNode }) => <div data-testid="sidebar-provider">{children}</div>,
  SidebarInset: ({ children }: { children: React.ReactNode }) => <main data-testid="sidebar-inset">{children}</main>,
}))

// Mock the AppSidebar component
jest.mock('@/components/app-sidebar', () => ({
  AppSidebar: () => <aside data-testid="app-sidebar">AppSidebar</aside>,
}))

describe('SidebarLayout', () => {
  it('renders the sidebar layout with provider and inset', () => {
    render(
      <SidebarLayout>
        <div>Test Content</div>
      </SidebarLayout>
    )
    
    expect(screen.getByTestId('sidebar-provider')).toBeInTheDocument()
    expect(screen.getByTestId('app-sidebar')).toBeInTheDocument()
    expect(screen.getByTestId('sidebar-inset')).toBeInTheDocument()
    expect(screen.getByText('Test Content')).toBeInTheDocument()
  })

  it('wraps children in sidebar inset', () => {
    render(
      <SidebarLayout>
        <h1>Main Content</h1>
        <p>Content paragraph</p>
      </SidebarLayout>
    )
    
    const inset = screen.getByTestId('sidebar-inset')
    expect(inset).toContainElement(screen.getByText('Main Content'))
    expect(inset).toContainElement(screen.getByText('Content paragraph'))
  })
})