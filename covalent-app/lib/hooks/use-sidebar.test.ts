import { renderHook } from '@testing-library/react'
import { useSidebar } from './use-sidebar'

// Mock the shadcn sidebar hook since we're just wrapping it
jest.mock('@/components/ui/sidebar', () => ({
  useSidebar: jest.fn(() => ({
    state: 'expanded',
    open: true,
    setOpen: jest.fn(),
    openMobile: false,
    setOpenMobile: jest.fn(),
    isMobile: false,
    toggleSidebar: jest.fn(),
  })),
}))

describe('useSidebar', () => {
  it('should return sidebar state and methods', () => {
    const { result } = renderHook(() => useSidebar())
    
    expect(result.current).toHaveProperty('state')
    expect(result.current).toHaveProperty('open')
    expect(result.current).toHaveProperty('setOpen')
    expect(result.current).toHaveProperty('openMobile')
    expect(result.current).toHaveProperty('setOpenMobile')
    expect(result.current).toHaveProperty('isMobile')
    expect(result.current).toHaveProperty('toggleSidebar')
  })
  
  it('should return expanded state by default', () => {
    const { result } = renderHook(() => useSidebar())
    
    expect(result.current.state).toBe('expanded')
    expect(result.current.open).toBe(true)
  })
})